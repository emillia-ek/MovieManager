<template>
  <div>
    <h1>Movies List</h1>
    <button @click="addNewMovie()">Add Movie</button>
    <table class="table-auto">
      <thead>
        <tr>
          <th>ID</th>
          <th>Title</th>
          <th>Director</th>
          <th>Year</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="movie in movies" :key="movie.id">
          <td>{{ movie.id }}</td>
          <td>{{ movie.title }}</td>
          <td>{{ movie.director }}</td>
          <td>{{ movie.year }}</td>
          <td><button @click="editMovie(movie)">Edit</button></td>
          <td><button @click="deleteMovie(movie.id)">Delete</button></td>
        </tr>
      </tbody>
    </table>

    <div class="modal fade" id="movieModal" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ isEditMode ? 'Edit movie' : 'Add movie' }}</h5>
            <button type="button" class="btn-close" @click="closeModal"></button>
          </div>
          <div class="modal-body">
            <div class="mb-3">
              <label for="movieTitle" class="form-label">Title:</label>
              <input type="text" id="movieTitle" class="form-control" v-model="selectedMovie.title">
            </div>
            <div class="mb-3">
              <label for="movieDirector" class="form-label">Director:</label>
              <input type="text" id="movieDirector" class="form-control" v-model="selectedMovie.director">
            </div>
            <div class="mb-3">
              <label for="movieYear" class="form-label">Year:</label>
              <input type="number" id="movieYear" class="form-control" v-model="selectedMovie.year">
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="closeModal">Cancel</button>
            <button type="button" class="btn btn-primary" @click="saveMovie">{{ isEditMode ? 'Save changes' : 'Add movie' }}</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
    import axios from "axios";
    import { Modal } from 'bootstrap';
    
    export default {
      data() {
        return {
          isEditMode: false,
          movies: [],
          selectedMovie: { id: null, title: '', director: '', year: '', rate: '' },
          modalInstance: null
        };
      },
      created() {
        this.fetchMovies();
      },
      async mounted() {
        const modalElement = document.getElementById('movieModal');
        if (modalElement) {
          this.modalInstance = new Modal(modalElement);
        } else {
          console.error("Modal element not found");
        }
        
        this.fetchMovies();
      },
      methods: {
        async fetchMovies() {
          try {
            const response = await axios.get("/api/Movies");
            this.movies = response.data;
          } catch (error) {
            console.error("Error fetching movies:", error);
          }
        },
        openModal() {
          if (this.modalInstance) {
            this.modalInstance.show();
          }
        },
        closeModal() {
          if (this.modalInstance) {
            this.modalInstance.hide();
          }
        },
        async addNewMovie(){
          this.isEditMode = false;
          this.selectedMovie = {title: '', director: '', year: '', rate: ''};
          this.openModal();
        },
        async deleteMovie(id) {
          if (confirm('Do you want to delete this movie?')){
            await axios.delete(`/api/Movies/${id}`);
            alert('Movie saved successfully!');
            await this.fetchMovies();
          }
        },
        editMovie(movie) {
          this.selectedMovie = { ...movie };
          this.isEditMode = true;
          this.openModal();
        },
        async saveMovie() {
          try {
            if (this.isEditMode) {
              await axios.put(`/api/Movies/${this.selectedMovie.id}`, this.selectedMovie);
            } else {
              await axios.post("/api/Movies", this.selectedMovie);
            }
            this.fetchMovies(); 
            this.closeModal();
            alert('Movie saved successfully!');
          } catch (error) {
            console.error('Error saving movie:', error);
            alert('Failed to save movie');
          }
        },
      },
    };
    </script>
    

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
}
th, td {
  padding: 10px;
  text-align: left;
  border: 1px solid #ddd;
}
th {
  background-color: #f4f4f4;
}
</style>
