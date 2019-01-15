import React from 'react';
import Header from './static/Header';
import Body from './dynamic/Body';
import _ from 'lodash';

export default class DynamicForm extends React.Component {

    constructor(props) {

        super(props);

        this.state = {
            selectedDepartmentId: null
        };

        this.onChangeDepartment = this.onChangeDepartment.bind(this);
        this.saveFormData = this.saveFormData.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
    }

    componentDidMount() {
        let url = this.props.baseUrl + "GetEmployeeData";

        if(this.props.id){
            url = url + "/" +  this.props.id;
        }

        fetch(url,
        {
            method: 'GET',
            header: {
                'Accept' : 'application/json',
                'Content-type' : 'application/json'
        }})
        .then(json => json.json())
        .then(data => {
            this.setState({
                employeeeData: data.employeeData,
                departmentsList: data.Departments,
                employeeName: data.employeeData.Name,
                selectedDepartmentId: data.employeeData && data.employeeData.EmployeeDepartment ? data.employeeData.EmployeeDepartment.ID : 0
            });
        });
    }

    getSelectedDepartment (departmentId) {
        if(departmentId === 0){
            return this.state.departmentsList[0];
        }

        return _.find(this.state.departmentsList, (x) => { return x.ID === departmentId});
    };

    onChangeDepartment = (event) => {
        this.setState({ selectedDepartmentId: parseInt(event.target.value) });
    };

    handleNameChange(event) {
        this.setState({employeeName: event.target.value});
    }

    saveFormData(data){

        const {id} = this.props;

        const employee = {
            name: this.state.employeeName,
            ID: id,
            employeeDepartment: {ID: this.state.selectedDepartmentId},
            employeeData: data.map(x =>{
                return _.assign(x, {field: {ID: x.fieldId}, fieldId: null});
            })
        };

        let type = "Create";

        if(this.props.id){
            type = "Edit";
        }

        fetch(this.props.baseUrl + "ChangeEmployee", {
            method: 'POST',
            body: JSON.stringify({employeeData: employee, changeType: type}),
            headers: {
                'Content-Type': 'application/json'
            }})
            .then(response => {
                return response;
            })
            .catch(err => err);
    }

    render() {
        if(!this.state || !this.state.employeeeData){
            return (<div>Loading...</div>);
        }

        const readOnly = this.props.type === "Read" ? true : false;
            
        const {employeeeData, employeeName, selectedDepartmentId, departmentsList} = this.state;

        const selectedDepartment = this.getSelectedDepartment(selectedDepartmentId);

        return (
            <div className="form-horizontal">
                <h4>Employee</h4>
                <hr />
                <Header
                    dateCreated={employeeeData.DateCreated}
                    selectedDepartmentId={selectedDepartmentId}
                    employeeName={employeeName}
                    handleNameChange={this.handleNameChange}
                    onChangeDepartment={this.onChangeDepartment}
                    departmentsList={departmentsList} 
                    isReadOnly={readOnly}/>
                <Body employeeeData={employeeeData} 
                    selectedDepartment={selectedDepartment} 
                    saveFormData={this.saveFormData} 
                    isReadOnly={readOnly} />
            </div>
        );
    }
}