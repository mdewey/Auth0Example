import React, { Component } from 'react'
import axios from 'axios'
import { Route } from 'react-router'
import { Layout } from './components/Layout'
import { Home } from './components/Home'
import { FetchData } from './components/FetchData'
import { Counter } from './components/Counter'

import auth from './Auth'

export default class App extends Component {
  static displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route exact path="/counter" component={Counter} />
        <Route exact path="/fetch-data" component={FetchData} />
        <Route exact path="/login" render={() => auth.login()} />
        <Route
          path="/logout"
          render={() => {
            auth.logout()
            return <p />
          }}
        />
        <Route
          path="/callback"
          render={() => {
            auth.handleAuthentication(() => {
              // // NOTE: Uncomment the following lines if you are using axios
              // //
              // // Set the axios authentication headers
              axios.defaults.headers.common = {
                Authorization: auth.authorizationHeader()
              }
            })
            return <p />
          }}
        />
      </Layout>
    )
  }
}
