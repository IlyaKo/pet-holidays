import React from 'react';
import { FaTrash, FaEdit } from 'react-icons/fa';

const DEFAULT_PET_PHOTO = "https://img.freepik.com/premium-psd/contact-icon-illustration-isolated_23-2151903357.jpg?w=740";


export function PetTable({ pets, onDelete, onEdit }) {
  return (
    <table className="table is-fullwidth is-striped">
      <thead>
        <tr>
        <th style={{ width: "350px" }}>Photo</th>
          <th>Name</th>
          <th>Type</th>
          <th>Age</th>
          <th>Weight</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {pets.map((pet) => (
          <tr key={pet.id}>
            <td>
              <figure className="image pet-photo">
                <img src={pet.photo || DEFAULT_PET_PHOTO} alt={pet.name || 'Pet image'} className="is-rounded" />
              </figure>
            </td>
            <td>{pet.name}</td>
            <td>{pet.petType.name}</td>
            <td>{pet.age} 5 years</td>
            <td>3kg</td>
            <td>
            <button
            onClick={() => onDelete(pet.id)}
             className="px-3"
            >
          <FaTrash className="mr-2" /> Delete
        </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default PetTable;