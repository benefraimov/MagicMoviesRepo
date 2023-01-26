import React, { useCallback, useEffect, useState } from "react";
import styles from "./Workers.module.css";
import Worker from "./Worker";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { popupIsOn } from "../../redux/actions/popupActions";

import axios from "axios";

const ChildMemo = React.memo(Worker);

export default function AllWorkers() {
  const [workers, setWorkers] = useState([]);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { userLogin, userIsAdmin, userDelete } = useSelector(
    (store) => store.worker
  );
  const [filteredWorkers, setFilteredWorkers] = useState([]);
  const [search, setSearch] = useState("");

  const getWorkers = async () => {
    try {
      const { data } = await axios.get("http://localhost:5000/api/workers");

      setWorkers(data);
    } catch (error) {
      dispatch(popupIsOn("Please check your internet connection."));
    }
  };

  useEffect(() => {
    getWorkers();
  }, [userDelete]);

  const searchShow = useCallback(() => {
    if (search) {
      const workersFound = workers.filter((worker) =>
        worker.fullName.toLowerCase().includes(search)
      );
      workersFound.length > 0
        ? setFilteredWorkers(workersFound)
        : setFilteredWorkers([]);
    } else {
      setFilteredWorkers([]);
    }
  }, [search, workers]);

  useEffect(() => {
    searchShow();
  }, [searchShow]);

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
      <div className={styles.workerGeneralContainer}>
        <div className={styles.searchFilter}>
          <h1>Search for a Worker</h1>
          <div className={styles.formInputGroup}>
            <input
              type="text"
              placeholder="Worker Name"
              className={styles.searchInput}
              required
              minLength="3"
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
          </div>
        </div>
        {/* Later Adding search filter method */}
        {filteredWorkers.length > 0
          ? filteredWorkers.map((worker) => {
              return (
                <ChildMemo
                  key={worker.workerId}
                  workerId={worker.workerId}
                  worker={worker}
                />
              );
            })
          : workers.length > 0 &&
            workers.map((worker) => {
              return (
                <ChildMemo
                  key={worker.workerId}
                  workerId={worker.workerId}
                  worker={worker}
                />
              );
            })}
      </div>
    </>
  );
}
