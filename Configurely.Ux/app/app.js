import React from 'react';
import ReactDOM from 'react-dom';
import DynamicForm from './Configurely/DynamicForm';

const root = document.getElementById('root');    
const id =  root.getAttribute('data-param');
const type =  root.getAttribute('data-type');

ReactDOM.render(<DynamicForm baseUrl={"/Employee/"} id={id} type={type}/>, root);