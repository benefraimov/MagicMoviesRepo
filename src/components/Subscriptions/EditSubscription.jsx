import React, { useEffect, useState, useCallback } from "react";
import { useNavigate, useParams } from "react-router-dom";
import styles from "./Subscriptions.module.css";
import { useDispatch, useSelector } from "react-redux";
import { popupIsOn } from "../../redux/actions/popupActions";

import axios from "axios";

export default function EditSubscription() {
  const navigate = useNavigate();
  const { userLogin } = useSelector((store) => store.worker);
  const dispatch = useDispatch();
  const { updateSubs } = useSelector((store) => store.worker.permissions);
  const { subscriberId } = useParams();
  const [values, setValues] = useState({
    fullName: "",
    email: "",
    city: "",
    createdDate: "",
    movieSubscribers: [],
    movies: [],
  });

  const handleChange = (e) => {
    setValues((oldValues) => ({
      ...oldValues,
      [e.target.id]: e.target.value,
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    const data = await axios.put(
      `http://localhost:5000/api/subscribers/${subscriberId}`,
      values
    );
    if (data.status === 200) {
      dispatch(popupIsOn("Subscriber updated successfully."));
      navigate(-1);
    }
  };

  const cancelForm = () => {
    navigate(-1);
  };

  const getSubscriberData = useCallback(async () => {
    const { data } = await axios.get(
      `http://localhost:5000/api/subscribers/${subscriberId}`
    );

    if (data) {
      let newObjData = {
        city: data.city,
        createdDate: data.createdDate,
        email: data.email,
        fullName: data.fullName,
        movieSubscribers: data.movies,
        movies: [],
      };
      setValues(newObjData);
    }
  }, [subscriberId]);

  useEffect(() => {
    getSubscriberData();
  }, [getSubscriberData]);

  const checkLogin = useCallback(() => {
    if (!userLogin) {
      navigate("/");
    }
  }, [userLogin, navigate]);

  useEffect(() => {
    checkLogin();
  }, [checkLogin]);

  const checkPermission = useCallback(() => {
    if (!updateSubs) {
      navigate(-1);
    }
  }, [updateSubs, navigate]);

  useEffect(() => {
    checkPermission();
  }, [checkPermission]);

  return (
    <>
      <form className={styles.formContainer} onSubmit={handleSubmit}>
        <div className={styles.formInputGroup}>
          <input
            type="text"
            placeholder="Full Name"
            id="fullName"
            className={styles["form-input"]}
            required
            minLength="5"
            value={values.fullName}
            onChange={handleChange}
          />
          <label htmlFor="fullName" className={styles["form-label"]}>
            Full Name
          </label>
        </div>
        <div className={styles.formInputGroup}>
          <input
            type="email"
            placeholder="Email"
            id="email"
            className={styles["form-input"]}
            required
            minLength="10"
            value={values.email}
            onChange={handleChange}
          />
          <label htmlFor="email" className={styles["form-label"]}>
            Email
          </label>
        </div>
        <div className={styles.formInputGroup}>
          <input
            type="text"
            placeholder="City"
            id="city"
            className={styles["form-input"]}
            required
            minLength="4"
            value={values.city}
            onChange={handleChange}
          />
          <label htmlFor="city" className={styles["form-label"]}>
            City
          </label>
        </div>
        <div className={styles.formInputGroup}>
          <input
            type="date"
            placeholder="Created Date"
            id="createdDate"
            className={styles["form-input"]}
            required
            value={values.createdDate.substring(0, 10)}
            onChange={handleChange}
          />
          <label htmlFor="createdDate" className={styles["form-label"]}>
            Created Date
          </label>
        </div>
        <div className={styles.formBtnGroup}>
          <button type="submit">Update Subscriber</button>
          <button type="button" onClick={cancelForm}>
            Cancel
          </button>
        </div>
      </form>
    </>
  );
}
