import React, { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import styles from "./Subscriptions.module.css";
import { popupIsOn } from "../../redux/actions/popupActions";

import axios from "axios";

export default function CreateSubscription() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { userLogin } = useSelector((store) => store.worker);
  const { createSubs } = useSelector((store) => store.worker.permissions);
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
    try {
      event.preventDefault();
      const { data } = await axios.post(
        "http://localhost:5000/api/subscribers",
        values
      );

      if (data.success) {
        dispatch(popupIsOn("Subscriber created successfully!"));
        navigate("/workers/allworkers");
      } else {
        dispatch(
          popupIsOn(
            "There was a problem creating a new Subscriber, You can try again please.."
          )
        );
      }
    } catch (error) {
      dispatch(popupIsOn("Please check your internet connection."));
    }
  };

  const cancelForm = () => {
    navigate("/subscriptions/allsubscriptions");
  };

  const checkLogin = useCallback(() => {
    if (!userLogin) {
      navigate("/");
    }
  }, [userLogin, navigate]);

  useEffect(() => {
    checkLogin();
  }, [checkLogin]);

  const checkPermission = useCallback(() => {
    if (!createSubs) {
      navigate(-1);
    }
  }, [createSubs, navigate]);

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
            value={values.createdDate}
            onChange={handleChange}
          />
          <label htmlFor="createdDate" className={styles["form-label"]}>
            Created Date
          </label>
        </div>
        <div className={styles.formBtnGroup}>
          <button type="submit">Create Subscriber</button>
          <button type="button" onClick={cancelForm}>
            Cancel
          </button>
        </div>
      </form>
    </>
  );
}
