import React, { Component } from 'react';
import PizzaOrder from './PizzaOrder';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { info: {}, loading: true };
  }

  componentDidMount() {
    this.populateInfo();
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <PizzaOrder info={this.state.info}></PizzaOrder>

    return (
      <div className="row">
        <div className="col-12">
        <img
          src="https://static.vecteezy.com/system/resources/previews/012/464/763/non_2x/top-view-of-a-pizza-with-various-ingredients-a-whole-pizza-with-mushrooms-tomatoes-onions-peppers-and-cheese-italian-pizza-illustration-in-cartoon-style-vector.jpg"
          className="img-fluid mx-auto d-block"
          style={{height:300}}
          alt='Large Pizza'
        ></img>
        </div>
        
      {contents}
      </div>
    );
  }

  async populateInfo() {
    const response = await fetch('api/pizza/info');
    const data = await response.json();
    this.setState({ info: data, loading: false });
  }
}
