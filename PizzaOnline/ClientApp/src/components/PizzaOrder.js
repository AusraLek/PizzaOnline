import { useState } from 'react';

export default function PizzaOrder({ info }) {
    const [size, setSize] = useState(info.sizes[0]);
    const [toppings, setToppings] = useState([]);
    const [price, setPrice] = useState(0);

    function handleSizeClick(event) {
        setSize(event.target.value);
        getPrice(event.target.value, toppings);
    }

    function handleToppingClick(event) {
        const newToppings = [...toppings];
        if(!newToppings.includes(event.target.value)){
            newToppings.push(event.target.value);
        }else{
            newToppings.splice(newToppings.indexOf(event.target.value), 1);
        }
        setToppings(newToppings);
        getPrice(size, newToppings);
    }

    function getPrice(selectedSize, selectedToppings) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ selectedSize: selectedSize, selectedToppings: selectedToppings })
        };
        fetch('/api/pizza/price', requestOptions)
            .then(response => response.json())
            .then(data => setPrice(data));
    }

    function placeOrder() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ selectedSize: size, selectedToppings: toppings })
        };
        fetch('/api/pizza/order', requestOptions)
            .then(response => {
              setSize(info.sizes[0]);
              setToppings([]);
              setPrice(0);
            });
    }

    return (
       <>
        <div className="col-12 text-center m-2">
          {
            info.sizes.map(item => (
              <div className="form-check form-check-inline" key={item}>
                <input className="form-check-input" type="radio" name="inlineRadioOptions" value={item} onClick={handleSizeClick} checked={size === item}></input>
                <label className="form-check-label" >{item}</label>
              </div>
            ))
          }
        </div>
        <div className="row justify-content-center m-2">
          <div className="col-1">
          {
            info.toppings.map(item => (
              <div className="form-check" key={item}>
                <input className="form-check-input" type="checkbox" value={item} onClick={handleToppingClick} checked={toppings.includes(item)}></input>
                <label className="form-check-label">{item}</label>
              </div>
            ))
          }
          </div>
        </div>
        <div className="col-12 text-center m-2">Price: {price.toFixed(2)} €</div>
        <div className="col-12 text-center">
          <button type="button" className="btn btn-lg btn-success" onClick={placeOrder}>Order</button>
        </div>
      </>
    );
  }