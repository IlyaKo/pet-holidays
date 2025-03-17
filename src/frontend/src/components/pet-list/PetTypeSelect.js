import React from 'react';

function PetTypeSelect({ value, onChange, petTypes }) {
  return (
    <div className="field">
      <label className="label">Pet Type</label>
      <div className="control">
        <div className="select is-fullwidth">
          <select value={value} onChange={onChange}>
            <option value="">Select Pet Type</option>
            {petTypes.map((type) => (
              <option key={type.id} value={type.id}>
                {type.name}
              </option>
            ))}
          </select>
        </div>
      </div>
    </div>
  );
}

export default PetTypeSelect;