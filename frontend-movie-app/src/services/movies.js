import axios from "axios";

const API_URL = "/api/Movies";

export const fetchMovies = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching movies:", error);
    throw new Error("Error downloading movies.");
  }
};

export const addNewMovie = async (movie) => {
  try {
    const response = await axios.post(API_URL, movie);
    return response.data;
  } catch (error) {
    console.error("Error adding movie:", error);
    throw new Error("Error adding movie.");
  }
};

export const deleteMovie = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error("Error deleting movie:", error);
    throw new Error("Error deleting movie.");
  }
};

export const updateMovie = async (movie) => {
  try {
    const response = await axios.put(`${API_URL}/${movie.id}`, movie);
    return response.data;
  } catch (error) {
    console.error("Error updating movie:", error);
    throw new Error("Failed to update movie.");
  }
};
