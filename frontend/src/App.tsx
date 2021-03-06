import './App.scss';
import 'react-bootstrap-typeahead/css/Typeahead.css';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { Col } from 'react-bootstrap';
import configureStore from 'configureStore';
import { getActivateUserAction } from 'actionCreators/usersActionCreator';
import { getFetchLookupCodeAction } from 'actionCreators/lookupCodeActionCreator';
import useKeycloakWrapper from 'hooks/useKeycloakWrapper';
import { AuthStateContext, IAuthState } from 'contexts/authStateContext';
import AppRouter from 'router';
import OnLoadActions from 'OnLoadActions';
import { ToastContainer } from 'react-toastify';
import PublicLayout from 'layouts/PublicLayout';
import FilterBackdrop from 'components/maps/leaflet/FilterBackdrop';

export const store = configureStore();

const App = () => {
  const keycloakWrapper = useKeycloakWrapper();
  const keycloak = keycloakWrapper.obj;
  const dispatch = useDispatch();

  useEffect(() => {
    if (keycloak?.authenticated) {
      dispatch(getActivateUserAction());
      dispatch(getFetchLookupCodeAction());
    }
  }, [dispatch, keycloak]);

  return (
    <AuthStateContext.Consumer>
      {(context: IAuthState) => {
        if (!context.ready) {
          return (
            <PublicLayout>
              <Col>
                <FilterBackdrop show={true}></FilterBackdrop>
              </Col>
            </PublicLayout>
          );
        }

        return (
          <>
            <AppRouter />
            <OnLoadActions />
            <ToastContainer
              position="top-right"
              autoClose={5000}
              hideProgressBar
              newestOnTop={false}
              closeOnClick={false}
              rtl={false}
              pauseOnFocusLoss={false}
              draggable
              pauseOnHover
            />
          </>
        );
      }}
    </AuthStateContext.Consumer>
  );
};

export default App;
