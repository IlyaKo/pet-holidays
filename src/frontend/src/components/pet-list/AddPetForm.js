import React, { useState } from 'react';
import PetTypeSelect from './PetTypeSelect';

function AddPetForm({ onAddPet, petTypes }) {
  const [newPetName, setNewPetName] = useState('');
  const [newPetDescription, setNewPetDescription] = useState('');
  const [newPetTypeId, setNewPetTypeId] = useState('');
  const [petTypeRequiredError, setPetTypeRequiredError] = useState(false);

  const handleAddPet = (e) => {
    e.preventDefault();

    if (!newPetTypeId) {
      setPetTypeRequiredError(true);
      return;
    }

    setPetTypeRequiredError(false);

    onAddPet({
      name: newPetName,
      description: newPetDescription,
      petTypeId: newPetTypeId,
    });
    setNewPetName('');
    setNewPetDescription('');
    setNewPetTypeId('');
  };

  return (
    <form onSubmit={handleAddPet} className="box">
      <h2 className="title is-4">Add New Pet</h2>

      <div className="field">
        <label className="label">Pet Name</label>
        <div className="control">
          <input
            className="input"
            value={newPetName}
            onChange={(e) => setNewPetName(e.target.value)}
            required
            placeholder="Enter pet name"
          />
        </div>
      </div>

      <PetTypeSelect
        value={newPetTypeId}
        onChange={(e) => setNewPetTypeId(e.target.value)}
        petTypes={petTypes}
      />

      {petTypeRequiredError && (
        <p className="help is-danger mt-1">
          Please select a pet type.
        </p>
      )}

      <div className="field">
        <label className="label">Pet Description</label>
        <div className="control">
          <textarea
            className="textarea"
            value={newPetDescription}
            onChange={(e) => setNewPetDescription(e.target.value)}
            placeholder="Enter pet description"
            autoComplete="off"
          />
        </div>
      </div>

      <div className="field">
        <div className="control">
          <button className="button is-success">Add Pet</button>
        </div>
      </div>
    </form>
  );
}

export default AddPetForm;