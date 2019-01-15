import React from 'react';
import * as controls from "../common/controls";
import _ from 'lodash';
import merge from "lodash/merge";

export default class Body extends React.Component {

    constructor(props) {

        super(props);

        this.state = {
            fields: null,
            fieldsData: null
        };

        this.generateControls = this.generateControls.bind(this);
        this.getControl = this.getControl.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.setFields = this.setFields.bind(this);
    }

    componentDidMount(){
        this.setFields();
    }

    componentDidUpdate(prevProps){

        if (this.props.selectedDepartment.ID != prevProps.selectedDepartment.ID){
            this.setFields();
        }
    }

    setFields(){
        const {employeeeData, selectedDepartment} = this.props;

        let fields = null;
        let data = [];

        if(employeeeData.EmployeeData && employeeeData.EmployeeData.length > 0){
            fields = employeeeData.EmployeeData.map(x => x.Field);
        }
        else{
            if(employeeeData.EmployeeDepartment && employeeeData.EmployeeDepartment.ID === selectedDepartment.ID){
                fields = employeeeData.EmployeeDepartment.Fields;
            }
            else{
                fields = selectedDepartment.Fields;
            }
        }

        let employeeId = employeeeData ? employeeeData.ID : 0;

        _.forEach(fields, (key) =>{

            const value = _.find(employeeeData.EmployeeData, (data) => {
                                return key.ID === data.Field.ID
                            });

            data.push({employeeId: employeeId, fieldId: key.ID, value: value ? value.Value : key.DefaultValue});
        });

        this.setState(merge({}, {fields: fields, fieldsData: data}, this.state.fields));
    }

    generateControls(fields, fieldsData){

        const sortedFields = _.orderBy(fields, ['Sort'], ['asc']);

        return(
            sortedFields.map((x, key) =>{

                const fieldData = _.find(fieldsData, (data) => {
                    return x.ID === data.fieldId
                });

                return this.getControl(x, fieldData.value);
            })
        );
    }

    getControl(field, fieldValue){
        const isReadOnly = this.props.isReadOnly;

        switch(field.Type.ID) {
            case 1:
                return controls.TextBox(field, this.handleInputChange, fieldValue, isReadOnly);
            case 2:
                return controls.Checkbox(field, this.handleInputChange, fieldValue, isReadOnly);
            case 3:
                return controls.Dropdown(field, this.handleInputChange, fieldValue, isReadOnly);
            case 4:
                return controls.RadioButton(field, this.handleInputChange, fieldValue, isReadOnly);
          }
    }

    handleInputChange(event, id) {
        const target = event.target;
        let value = null;
        
        if(target.type === 'checkbox'){
            const oldValue = _.find(this.state.fieldsData, (data) => {
                                return data.fieldId === id
                            });

            if(target.checked){
                value = oldValue.value + "|" + target.name;
            }
            else{
                let items = oldValue.value.split("|");
                _.remove(items, (n) => n === target.name);
                value = items.join("|");
            }
            
        }
        else if(target.type === 'radio'){
            value = target.labels[0].innerText;
        }
        else{
            value = target.value;
        }

        this.setState({
            fieldsData: this.state.fieldsData.map(el => (
                    el.fieldId === id ? Object.assign({}, el, { value: value }) : el)
                )
          });
    }

    render() {
        const {fields, fieldsData} = this.state;

        let controls = this.generateControls(fields, fieldsData);

        return (
            <React.Fragment>
                {controls}
                <div className="form-group">
                    <div className="col-md-offset-2 col-md-10">
                        <input type="submit" value="Create" 
                            className="btn btn-default"
                            disabled={this.props.isReadOnly}
                            onClick={() => this.props.saveFormData(fieldsData)}/>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}