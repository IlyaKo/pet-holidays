import { useEffect, useState } from "react";
import axios from "axios";
import { FaEdit, FaTrash, FaPlus } from "react-icons/fa";

const api = axios.create({
  baseURL: "http://localhost:5001/api",
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

const DEFAULT_PET_PHOTO = "https://img.freepik.com/free-vector/user-circles-set_78370-4704.jpg?t=st=1741525335~exp=1741528935~hmac=0fa9bd3b7b4e2b4ce31ed123b0c656b733ef73095141a5822a015ab4f1a545df&w=740"; // Фото по умолчанию

export default function PetsPage() {
  const [pets, setPets] = useState([]);
  const [newPet, setNewPet] = useState({ name: "", petTypeId: "", photo: "" });
  const [editingPet, setEditingPet] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchPets();
  }, []);

  const fetchPets = async () => {
    try {
      const response = await api.get("/pets");
      setPets(response.data);
    } catch (error) {
      console.error("Ошибка при загрузке питомцев:", error);
      setError("Не удалось загрузить список питомцев.");
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewPet((prev) => ({
      ...prev,
      [name]: name === "petTypeId" ? Number(value) : value,
    }));
  };

  const handleCreateOrUpdatePet = async () => {
    setError("");
    if (!newPet.name || !newPet.petTypeId) {
      setError("Введите имя и ID типа питомца.");
      return;
    }

    try {
      if (editingPet) {
        await api.put(`/pets/${editingPet.id}`, newPet);
      } else {
        await api.post("/pets", newPet);
      }

      setNewPet({ name: "", petTypeId: "", photo: "" });
      setEditingPet(null);
      fetchPets();
    } catch (error) {
      console.error("Ошибка при сохранении питомца:", error);
      setError("Ошибка при сохранении питомца.");
    }
  };

  const handleEdit = (pet) => {
    setNewPet({ name: pet.name, petTypeId: pet.petTypeId, photo: pet.photo || "" });
    setEditingPet(pet);
  };

  const handleDelete = async (id) => {
    try {
      await api.delete(`/pets/${id}`);
      fetchPets();
    } catch (error) {
      console.error("Ошибка при удалении питомца:", error);
      setError("Ошибка при удалении питомца.");
    }
  };

  return (
    <div className="max-w-xl mx-auto p-6">
      <h1 className="text-4xl font-bold text-center mb-6">🐶 Мои питомцы 🐱</h1>

      <div className="space-y-4">
        {pets.map((pet) => (
          <div
            key={pet.id}
            className="bg-white border-2 border-gray-300 shadow-lg rounded-lg p-6 flex flex-col items-center transition hover:scale-105"
          >
            <img
              src={pet.photo || DEFAULT_PET_PHOTO}
              alt={pet.name}
              className="w-32 h-32 object-cover rounded-full border-2 border-gray-400"
            />
            <h2 className="text-2xl font-semibold mt-4">{pet.name}</h2>
            <p className="text-xl text-gray-600">Тип: {pet.petTypeId}</p>
            <div className="flex gap-6 mt-4">
              <button className="text-blue-500 hover:text-blue-700" onClick={() => handleEdit(pet)}>
                <FaEdit size={28} />
              </button>
              <button className="text-red-500 hover:text-red-700" onClick={() => handleDelete(pet.id)}>
                <FaTrash size={28} />
              </button>
            </div>
          </div>
        ))}
      </div>

      <div className="mt-10 bg-gray-100 p-6 rounded-lg shadow-md border-2 border-gray-300">
        {error && <div className="text-red-600 font-semibold text-lg mb-4">{error}</div>}

        <h2 className="text-2xl font-semibold mb-4">{editingPet ? "Редактировать" : "Добавить питомца"}</h2>

        <input
          type="text"
          name="name"
          value={newPet.name}
          onChange={handleInputChange}
          placeholder="Имя питомца"
          className="border-2 p-3 rounded w-full mb-4 text-xl"
        />
        <input
          type="number"
          name="petTypeId"
          value={newPet.petTypeId}
          onChange={handleInputChange}
          placeholder="ID типа питомца"
          className="border-2 p-3 rounded w-full mb-4 text-xl"
        />
        <input
          type="text"
          name="photo"
          value={newPet.photo}
          onChange={handleInputChange}
          placeholder="Ссылка на фото (необязательно)"
          className="border-2 p-3 rounded w-full mb-4 text-xl"
        />

        <button
          onClick={handleCreateOrUpdatePet}
          className="bg-green-500 text-white px-4 py-3 rounded w-full hover:bg-green-600 text-xl flex items-center justify-center gap-2"
        >
          {editingPet ? "Сохранить" : "Добавить"} <FaPlus size={24} />
        </button>
      </div>
    </div>
  );
}
