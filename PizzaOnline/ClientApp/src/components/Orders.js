import React, { Component } from 'react';

export class Orders extends Component {
  static displayName = Orders.name;

  constructor(props) {
    super(props);
    this.state = { orders: [], loading: true };
  }

  componentDidMount() {
    this.populateOrderData();
  }

  static renderOrdersTable(orders) {
    return (
      <table className='table table-hover' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Date</th>
            <th>Size</th>
            <th>Toppings</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>
          {orders.map(order =>
            <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.timestamp}</td>
              <td>{order.size}</td>
              <td>{order.toppings}</td>
              <td>{order.price.toFixed(2)} €</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Orders.renderOrdersTable(this.state.orders);

    return (
      <div>
        <h1 id="tabelLabel" >Order history</h1>
        {contents}
      </div>
    );
  }

  async populateOrderData() {
    const response = await fetch('api/pizza/orders');
    const data = await response.json();
    this.setState({ orders: data, loading: false });
  }
}
