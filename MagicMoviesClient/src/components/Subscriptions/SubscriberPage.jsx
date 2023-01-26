import React, { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import styles from "./Subscriptions.module.css";
import Movie from "../Movies/Movie";
import { useDispatch, useSelector } from "react-redux";
import { popupIsOn } from "../../redux/actions/popupActions";
import axios from "axios";

const ChildMemo = React.memo(Movie);

export default function SubscriberPage() {
  const { subscriberId } = useParams();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { userLogin } = useSelector((store) => store.worker);
  const [values, setValues] = useState({
    fullName: "",
    email: "",
    city: "",
    createdDate: "",
    movieSubscribers: [],
    movies: [],
  });
  const [movies, setMovies] = useState([]);
  const [movieToAddName, setMovieToAddName] = useState("");
  const [movieToDeleteName, setMovieToDeleteName] = useState("");

  const getSubscriberData = useCallback(async () => {
    try {
      const subsData = await axios.get(
        `http://localhost:5000/api/subscribers/${subscriberId}`
      );

      if (subsData.status === 200) {
        let newObjData = {
          fullName: subsData.data.fullName,
          email: subsData.data.email,
          city: subsData.data.city,
          createdDate: subsData.data.createdDate.substring(0, 10),
          movieSubscribers: subsData.data.movies,
          movies: [],
        };

        setValues(newObjData);
      }

      const moviesData = await axios.get("http://localhost:5000/api/movies");

      if (moviesData.status === 200) setMovies(moviesData.data);
    } catch (error) {
      dispatch(popupIsOn("Please check your internet connection."));
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

  const addMovie = async () => {
    const movie = movies.filter((movie) => movie.name === movieToAddName)[0];

    const objToUpdate = {
      ...values,
      movieSubscribers: [...values.movieSubscribers, movie],
    };

    const { data } = await axios.put(
      `http://localhost:5000/api/subscribers/${subscriberId}`,
      objToUpdate
    );

    if (data.success) {
      dispatch(popupIsOn("Movie add to subscriber seccessfully."));
      navigate(-1);
    }
  };

  const deleteMovie = async () => {
    const movie = movies.filter((movie) => movie.name === movieToDeleteName)[0];

    const movieSubscribersUpdated = values.movieSubscribers.filter(
      (movieElement) => movieElement.name !== movie.name
    );

    const objToUpdate = {
      ...values,
      movieSubscribers: movieSubscribersUpdated,
    };

    const { data } = await axios.put(
      `http://localhost:5000/api/subscribers/${subscriberId}`,
      objToUpdate
    );

    if (data.success) {
      dispatch(popupIsOn("Movie deleted from subscriber seccessfully."));
      navigate(-1);
    }
  };

  const handleAddMovieChange = (e) => {
    setMovieToAddName(e.target.value);
  };

  const handleDeleteMovieChange = (e) => {
    setMovieToDeleteName(e.target.value);
  };

  return (
    <>
      <div className={styles.subscriptionPageContainer}>
        <div className={styles.subscriptionPageDetails}>
          <ul>
            <li>
              <span>Full Name: </span>
              {values.fullName}
            </li>
            <li>
              <span>Email: </span>
              {values.email}
            </li>
            <li>
              <span>City: </span> {values.city}
            </li>
            <li>
              <span>Created Date: </span>{" "}
              {values.createdDate && values.createdDate.substring(0, 10)}
            </li>
            <li>
              <span>Add Movie From The List: </span>
              <select value={movieToAddName} onChange={handleAddMovieChange}>
                <option key={-1} value={""}>
                  Please choose a movie
                </option>
                {movies.length > 0 &&
                  movies.map((movie) => {
                    return (
                      <option key={movie.movieId} value={movie.name}>
                        {movie.name}
                      </option>
                    );
                  })}
              </select>
              <button onClick={addMovie}>Add</button>
            </li>
            <li>
              <span>Delete Movie From The List: </span>
              <select
                value={movieToDeleteName}
                onChange={handleDeleteMovieChange}>
                <option key={-1} value={""}>
                  Please choose a movie
                </option>
                {values.movieSubscribers.length > 0 &&
                  values.movieSubscribers.map((movie) => {
                    return (
                      <option key={movie.movieId} value={movie.name}>
                        {movie.name}
                      </option>
                    );
                  })}
              </select>
              <button onClick={deleteMovie}>Delete</button>
            </li>
          </ul>
        </div>
        {values.movieSubscribers.length > 0 &&
          values.movieSubscribers.map((movie) => {
            return <ChildMemo key={movie.movieId} movieId={movie.movieId} />;
          })}
      </div>
    </>
  );
}
