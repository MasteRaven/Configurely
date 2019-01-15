import React from 'react';
import _ from 'lodash';

export const TextBox = (field, handleInputChange, fieldValue, isReadOnly) => {

    const fieldID = field.ID;

    const currentValue = fieldValue ? fieldValue : field.DefaultValue;

    return(
        <div className="form-group">
            <label className="control-label col-md-2" for={field.Name}>{field.Name}</label>
            <div className="col-md-10">
                <input className="form-control text-box single-line" id={field.Name} name={field.Name}
                    readOnly={isReadOnly} type="text" value={currentValue ? currentValue : ""}
                    onChange={(e) => handleInputChange(e, fieldID)}/>
            </div>
        </div>
    );
};

export const Checkbox = (field, handleInputChange, fieldValue, isReadOnly) => {

    const fieldID = field.ID;

    const values = _.split(field.Value, "|");

    const currentValues = fieldValue ? _.split(fieldValue, "|") : null;

    return(
        <div className="form-group">
            <label className="control-label col-md-2">{field.Name}</label>
            <div className="col-md-10">
                {values.map((x, key) => {
                    let isChecked = false;

                    const currentValue = _.find(currentValues, (value) => {
                        return x === value
                    });
                    
                    if(currentValue){
                        isChecked = true;
                    }
                    return (
                        <label className="checkbox-inline">
                            <input defaultChecked={isChecked} type="checkbox"
                                name={x} readOnly={isReadOnly}
                                onChange={(e) => handleInputChange(e, fieldID, x)} />
                        {x}</label>
                    );
                })}
            </div>
        </div>
    );
};

export const Dropdown = (field, handleInputChange, fieldValue, isReadOnly) => {

    const fieldID = field.ID;

    const values = _.split(field.Value, "|");

    let isSingleSelected = false;

    const currentValue = fieldValue ? fieldValue : field.DefaultValue;

    return(
        <div className="form-group">
            <label className="control-label col-md-2" for={field.Name}>{field.Name}</label>
            <div className="col-md-10">
                <select className="form-control" id={field.Name} disabled={isReadOnly}
                    onChange={(e) => handleInputChange(e, fieldID)}>
                        {values.map((x, key) => {
                            let isSelected = false;
                            if(!isSingleSelected && (currentValue && currentValue === x)){
                                isSelected = true;
                                isSingleSelected = true;
                            }
                            return <option selected={isSelected} key={key} value={x}>{x}</option>
                        })}
                </select>
            </div>
        </div>
    );
};

export const RadioButton = (field, handleInputChange, fieldValue, isReadOnly) => {

    const fieldID = field.ID;

    const values = _.split(field.Value, "|");

    let isUniqueChecked = false;

    const currentValue = fieldValue ? fieldValue : field.DefaultValue;

    return(
        <div className="form-group">
            <label className="control-label col-md-2">{field.Name}</label>
            <div className="col-md-10">
                {values.map((x, key) => {
                    let isChecked = false;
                    if(!isUniqueChecked && (currentValue && currentValue === x)){
                        isChecked = true;
                        isUniqueChecked = true;
                    }
                    return (
                        <label className="radio-inline">
                            <input checked={isChecked} readOnly={isReadOnly}
                            onChange={(e) => handleInputChange(e, fieldID, x)} 
                            type="radio" name={field.Name} />
                        {x}</label>);
                })}
            </div>
        </div>
    );
};