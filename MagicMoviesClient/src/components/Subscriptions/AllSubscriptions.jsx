import React, { useCallback, useEffect, useState } from "react";
import styles from "./Subscriptions.module.css";
import Subscription from "./Subscription";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { popupIsOn } from "../../redux/actions/popupActions";

import axios from "axios";

const ChildMemo = React.memo(Subscription);

export default function AllSubscriptions() {
  const [subscriptions, setSubscriptions] = useState([]);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { userLogin } = useSelector((store) => store.worker);
  const [filteredSubscriptions, setFilteredSubscriptions] = useState([]);
  const [search, setSearch] = useState("");

  const getSubscribers = async () => {
    try {
      const { data } = await axios.get("http://localhost:5000/api/subscribers");

      if (data.length > 0) {
        setSubscriptions(data);
      } else {
        dispatch(popupIsOn("There are no existed subscription."));
      }
    } catch (error) {
      dispatch(popupIsOn("Please check your internet connection."));
    }
  };

  useEffect(() => {
    getSubscribers();
  }, []);

  const searchShow = useCallback(() => {
    if (search) {
      const subscriptionsFound = subscriptions.filter((subscriber) =>
        subscriber.fullName.toLowerCase().includes(search)
      );
      subscriptionsFound.length > 0
        ? setFilteredSubscriptions(subscriptionsFound)
        : setFilteredSubscriptions([]);
    } else {
      setFilteredSubscriptions([]);
    }
  }, [search, subscriptions]);

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

  return (
    <>
      <div className={styles.subscriptionGeneralContainer}>
        <div className={styles.searchFilter}>
          <h1>Search for a Subscriber</h1>
          <div className={styles.formInputGroup}>
            <input
              type="text"
              placeholder="Subscriber Name"
              className={styles.searchInput}
              required
              minLength="3"
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
          </div>
        </div>
        {/* Later Adding search filter method */}
        {filteredSubscriptions.length > 0
          ? filteredSubscriptions.map((subscriber) => {
              return (
                <ChildMemo
                  key={subscriber.subscriberId}
                  subscriberId={subscriber.subscriberId}
                  subscriber={subscriber}
                />
              );
            })
          : subscriptions.length > 0 &&
            subscriptions.map((subscriber) => {
              return (
                <ChildMemo
                  key={subscriber.subscriberId}
                  subscriberId={subscriber.subscriberId}
                  subscriber={subscriber}
                />
              );
            })}
      </div>
    </>
  );
}

//watchMovie={openMoviePage}
