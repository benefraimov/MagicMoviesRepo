import React, { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import styles from "./Workers.module.css";
import { useDispatch, useSelector } from "react-redux";
import { popupIsOn } from "../../redux/actions/popupActions";
import axios from "axios";

export default function WorkerPage() {
  const { workerId } = useParams();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { userLogin, userIsAdmin } = useSelector((store) => store.worker);
  const [values, setValues] = useState({
    fullName: "",
    userName: "",
    password: "",
    createdDate: "",
    permissions: {
      watchSubs: true,
      createSubs: false,
      updateSubs: false,
      deleteSubs: false,
      watchMovies: true,
      createMovies: false,
      updateMovies: false,
      deleteMovies: false,
    },
  });

  const getWorkerData = useCallback(async () => {
    try {
      const { data } = await axios.get(
        `http://localhost:5000/api/workers/${workerId}`
      );
      if (data) setValues(data);
    } catch (error) {
      dispatch(popupIsOn("Please check your internet connection."));
    }
  }, [workerId]);

  useEffect(() => {
    getWorkerData();
  }, [getWorkerData]);

  const checkLogin = useCallback(() => {
    if (!userLogin) {
      navigate("/");
    }
  }, [userLogin, navigate]);

  useEffect(() => {
    checkLogin();
  }, [checkLogin]);

  const checkIsAdmin = useCallback(() => {
    if (!userIsAdmin) {
      navigate("/movies/allmovies");
    }
  }, [userIsAdmin, navigate]);

  useEffect(() => {
    checkIsAdmin();
  }, [checkIsAdmin]);

  return (
    <>
      <div className={styles.workerPageContainer}>
        <div className={styles.workerPageDetails}>
          <ul>
            <li>
              <span>Full Name: </span>
              {values.fullName}
            </li>
            <li>
              <span>Username: </span>
              {values.userName}
            </li>
            <li>
              <span>Password: </span>
              {values.password}
            </li>
            <li>
              <span>Created Date: </span> {values.createdDate.substring(0, 10)}
            </li>
          </ul>
          <h1 className={styles.permissionHeader}>Permissions</h1>
          <div className={styles.permissionsLists}>
            <ul className={styles.workerDetailsList}>
              <li>
                <span>watchSubs:</span>{" "}
                {values.permissions && values.permissions.watchSubs.toString()}
              </li>
              <li>
                <span>createSubs:</span>{" "}
                {values.permissions && values.permissions.createSubs.toString()}
              </li>
              <li>
                <span>updateSubs:</span>{" "}
                {values.permissions && values.permissions.updateSubs.toString()}
              </li>
              <li>
                <span>deleteSubs:</span>{" "}
                {values.permissions && values.permissions.deleteSubs.toString()}
              </li>
            </ul>
            <ul className={styles.workerDetailsList}>
              <li>
                <span>watchMovies:</span>{" "}
                {values.permissions &&
                  values.permissions.watchMovies.toString()}
              </li>
              <li>
                <span>createSubs:</span>{" "}
                {values.permissions &&
                  values.permissions.createMovies.toString()}
              </li>
              <li>
                <span>updateSubs:</span>{" "}
                {values.permissions &&
                  values.permissions.updateMovies.toString()}
              </li>
              <li>
                <span>deleteSubs:</span>{" "}
                {values.permissions &&
                  values.permissions.deleteMovies.toString()}
              </li>
            </ul>
          </div>
        </div>
      </div>
    </>
  );
}
