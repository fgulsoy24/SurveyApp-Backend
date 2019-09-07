
import ReactDOM from "react-dom";
import React, { Component } from 'react';
import Survey from "./survey"
import SurveyResults from "./surveyResults"
import { BrowserRouter as Router, Route, Link, Redirect } from 'react-router-dom';
import { Nav, NavItem } from "react-bootstrap"

class App extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (

      <Router>
        <section className="content">
          <div className = "container">
          <Nav bsStyle="pills">
            <NavItem eventKey={1} href="/survey">
              Survey
          </NavItem>
            <NavItem eventKey={2} href="/survey-results">
              Survey Results
         </NavItem>
          </Nav>
          </div>
          <br></br>

          <Route exact path="/survey-results" component={SurveyResults} />
          <Route exact path="/survey" component={Survey} />

        </section>

      </Router>
    )
  }
}

window.apiUrl = '';
ReactDOM.render(<App />, document.getElementById('content'));
export default App;