import {
  WORKER_DELETE,
  WORKER_DELETE_FAIL,
  WORKER_LOG_IN,
  WORKER_LOG_IN_FAIL,
  WORKER_LOG_OUT,
  WORKER_LOG_OUT_FAIL,
  WORKER_UPDATE,
  WORKER_UPDATE_DONE,
  WORKER_UPDATE_FAIL,
  WORKER_UPDATE_FAILED_IS_ADMIN,
  WORKER_UPDATE_FAILED_IS_ADMIN_DONE,
} from "../constants/workerConstants";
import axios from "axios";
import { POPUP_IS_ON } from "../constants/popupConstants";

export const logInAction =
  ({ userName, password }) =>
    async (dispatch) => {
      try {
        const { data } = await axios.get("http://localhost:5000/api/workers");

        const findOne = data.filter(
          (worker) => worker.userName === userName && worker.password === password
        );
        if (findOne.length > 0) {
          const fullName = findOne[0].fullName;
          const isAdmin = findOne[0].isAdmin;
          const permissions = findOne[0].permissions;
          dispatch({
            type: WORKER_LOG_IN,
            payload: {
              fullName,
              userName,
              password,
              isAdmin,
              permissions,
            },
          });
        } else {
          dispatch({
            type: WORKER_LOG_IN_FAIL,
            payload: "Username or Password is Incorrect",
          });
        }
      } catch (error) {
        dispatch({
          type: WORKER_LOG_IN_FAIL,
          payload:
            error.response && error.response.data.message
              ? error.response.data.message
              : error.message,
        });
      }
    };

export const logOutAction = () => (dispatch) => {
  try {
    dispatch({
      type: WORKER_LOG_OUT,
    });
  } catch (error) {
    dispatch({
      type: WORKER_LOG_OUT_FAIL,
      payload:
        error.response && error.response.data.message
          ? error.response.data.message
          : error.message,
    });
  }
};

export const deleteAction = (workerId) => async (dispatch) => {
  try {
    // fetch to somewhere just to delete the user and we will get response back, if the response will be confirmed, we will dispatch the payload(we need to send the workerId within the api url)

    let allUsers = await axios.get(`http://localhost:5000/api/workers`);

    if (allUsers.data.length < 2) {
      throw new Error(
        "This is a bad request, the system needs at least one user to exist!!"
      );
    }

    const { data } = await axios.delete(
      `http://localhost:5000/api/workers/${workerId}`
    );

    allUsers = await axios.get(`http://localhost:5000/api/workers`);

    if (allUsers.data.length === 1) {
      const fullName = allUsers.data[0].fullName;
      const userName = allUsers.data[0].userName;
      const password = allUsers.data[0].password;
      const isAdmin = true;
      const permissions = {
        watchSubs: true,
        createSubs: true,
        updateSubs: true,
        deleteSubs: true,
        watchMovies: true,
        createMovies: true,
        updateMovies: true,
        deleteMovies: true,
      };
      dispatch({
        type: WORKER_LOG_IN,
        payload: {
          fullName,
          userName,
          password,
          isAdmin,
          permissions,
        },
      });
    }

    dispatch({
      type: WORKER_DELETE,
      payload: data.message,
    });
  } catch (error) {
    dispatch({
      type: WORKER_DELETE_FAIL,
      payload:
        error.response && error.response.data.message
          ? error.response.data.message
          : error.message,
    });
  }
};

export const updateAction = (value, workerId) => async (dispatch) => {
  try {
    // fetch to update the user and if everything is updated, we can proceed with dispatching the information next to the reducer...
    const response = await axios.get(`http://localhost:5000/api/workers`);
    if (response.data.length === 1 && !value.isAdmin) {
      dispatch({
        type: WORKER_UPDATE_FAILED_IS_ADMIN,
      });

      setTimeout(() => {
        dispatch({
          type: WORKER_UPDATE_FAILED_IS_ADMIN_DONE,
        });
      }, [3000]);
      throw new Error("Cannot change the only admin user!!");
    }
    if (value.isAdmin) {
      value.permissions = {
        watchSubs: true,
        createSubs: true,
        updateSubs: true,
        deleteSubs: true,
        watchMovies: true,
        createMovies: true,
        updateMovies: true,
        deleteMovies: true,
      };

      dispatch({
        type: POPUP_IS_ON,
        payload: "Admin user must have all permissions.",
      });
    }

    const { data } = await axios.put(
      `http://localhost:5000/api/workers/${workerId}`,
      value
    );

    if (data.success) {
      dispatch({
        type: WORKER_UPDATE,
        payload: data.message,
      });

      setTimeout(() => {
        dispatch({
          type: WORKER_UPDATE_DONE,
        });
      }, [3000]);
    } else {
      throw new Error(data.message);
    }
  } catch (error) {
    dispatch({
      type: WORKER_UPDATE_FAIL,
      payload:
        error.response && error.response.data.message
          ? error.response.data.message
          : error.message,
    });
  }
};
