import React from 'react';
import ReactDOM from 'react-dom';
import {
  Route,
  BrowserRouter as Router,
  Redirect,
  Switch
} from 'react-router-dom';
import { parseJwt, usuarioAutenticado } from './services/auth';
import './index.css';
import reportWebVitals from './reportWebVitals';
import Login from './pages/login/login';
import Administrador from './pages/Administrador/adm';
import Medico from './pages/medico/medico'
import Paciente from './pages/paciente/paciente'
import Mapa from './pages/mapa/mapa'


const PermissaoAdm = ({ component: Component }) => (
  <Route
    render={(props) =>
      usuarioAutenticado() && parseJwt().role === '1' ? (
        <Component {...props} />
      ) : (
        <Redirect to="/login" />
      )
    }
  />
);
const PermissaoMedico = ({ component: Component }) => (
  <Route
    render={(props) =>
      usuarioAutenticado() && parseJwt().role === '3' ? (
        <Component {...props} />
      ) : (
        <Redirect to="/login" />
      )
    }
  />
);
const PermissaoPaciente = ({ component: Component }) => (
  <Route
    render={(props) =>
      usuarioAutenticado() && parseJwt().role === '2' ? (
        <Component {...props} />
      ) : (
        <Redirect to="/login" />
      )
    }
  />
);

const routing = (
  <Router>
    <div>
      <Switch>
        <Route path="/login" component={Login} />
        <PermissaoAdm path="/listarConsultas" component={Administrador} />
        <PermissaoAdm path="/mapa" component={Mapa} />
        <PermissaoMedico path="/minhasConsultasMedico" component={Medico} />
        <PermissaoPaciente path="/minhasConsultasPaciente" component={Paciente} />
        <Route exact patch="/"><Redirect to="/login"/></Route>
      </Switch>
    </div>
  </Router>
)

ReactDOM.render(
  routing,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
