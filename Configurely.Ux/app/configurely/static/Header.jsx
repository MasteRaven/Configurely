import React from 'react';

export default class DynamicForm extends React.Component {

    constructor(props) {

        super(props);
    }

    render() {
        const {employeeName, departmentsList, selectedDepartmentId, onChangeDepartment, handleNameChange} = this.props;

        const dateCreated = new Date(this.props.dateCreated).toLocaleString();

        return (
            <React.Fragment>
                <div className="form-group">
                    <label className="control-label col-md-2" for="Name">Name</label>
                    <div className="col-md-10">
                        <input readOnly={this.props.isReadOnly} className="form-control text-box single-line" 
                            id="Name" name="Name" type="text" value={employeeName} 
                            onChange={(e) => handleNameChange(e)}/>
                    </div>
                </div>
                <div className="form-group">
                    <label className="control-label col-md-2">DateCreated</label>
                    <div className="col-md-10">
                        <input readOnly className="form-control text-box single-line"
                            readOnly={this.props.isReadOnly}
                            id="DateCreated" name="DateCreated" type="text" value={dateCreated} />
                    </div>
                </div>
                <div className="form-group">
                    <label className="control-label col-md-2" for="departmentsIds">Department</label>
                    <div className="col-md-10">
                        <select disabled={this.props.isReadOnly} className="form-control" id="departmentsIds" 
                            onChange={onChangeDepartment}
                            value={selectedDepartmentId}>
                                {departmentsList.map(x => {
                                    return <option key={x.ID} value={x.ID}>{x.Name}</option>
                                })}
                        </select>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}