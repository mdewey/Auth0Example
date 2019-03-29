import React, { Component } from 'react'
import auth from '../Auth'
import axios from 'axios'
export class FetchData extends Component {
  static displayName = FetchData.name

  constructor(props) {
    super(props)
    this.state = { monsters: [], loading: true }
  }

  componentDidMount() {
    if (auth.isAuthenticated()) {
      axios.defaults.headers.common = {
        Authorization: auth.authorizationHeader()
      }
    }
    axios.get('api/ping').then(data => {
      // this.setState({ monsters: data.data, loading: false })
    })
  }

  static renderForecastsTable(monsters) {
    return (
      <table className="table table-striped">
        <thead>
          <tr>
            <th />
            <th />
            <th />
          </tr>
        </thead>
        <tbody>
          {monsters.map(monster => (
            <tr key={monster.id}>
              <td>{monster.name}</td>
              <td>{monster.cr}</td>
              <td>{monster.totalHealth}</td>
            </tr>
          ))}
        </tbody>
      </table>
    )
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchData.renderForecastsTable(this.state.monsters)
    )

    return (
      <div>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    )
  }
}
