<template>
  <div class="container mt-4">
    <h1 class="display-3 mt-5">Movies List</h1>

    <Alert :message="errorMessage" type="danger"/>
    <Alert :message="successMessage" type="success"/>

    <div class="mb-3">
      <button @click="addNewMovie" class="btn btn-primary ">Add Movie</button>
      <button @click="downloadMovies" class="btn btn-secondary m-4 ">Download Movies</button>
    </div>

    <table class="table table-hover">
      <thead>
        <tr>
          <th>ID</th>
          <th>Title</th>
          <th>Director</th>
          <th>Year</th>
          <th>Rate</th>
          <th>Actions</th>
        </tr>
      </thead>

      <tbody>
        <tr v-if="isLoading">
          <td colspan="6" class="text-center">
            <div class="spinner-border text-primary m-5" role="status">
              <span class="visually-hidden">Loading...</span>
            </div>
          </td>
        </tr>

        <tr v-else-if="movies.length === 0">
          <td colspan="6" class="text-center">No movies found.</td>
        </tr>
        
        <tr v-else v-for="movie in movies" :key="movie.id" class="align-middle">
          <td>{{ movie.id }}</td>
          <td>{{ movie.title }}</td>
          <td>{{ movie.director }}</td>
          <td>{{ movie.year }}</td>
          <td>{{ movie.rate }}</td>
          <td>
            <button @click="editMovie(movie)" class="btn btn-secondary btn-sm">Edit</button>
            <button @click="deleteMovie(movie.id)" class="btn btn-danger btn-sm m-2">Delete</button>
          </td>
        </tr>
      </tbody>

    </table>


    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="staticBackdropLabel">{{ isEditMode ? "Edit Movie" : "Add Movie" }}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="closeModal"></button>
          </div>
          <div class="modal-body">
            <div class="mb-3">
              <label for="movieTitle" class="form-label">Title:</label>
              <input type="text" id="movieTitle" class="form-control" v-model="selectedMovie.title" :class="{'is-invalid': v$.selectedMovie.title.$error}">
              <div v-if="v$.selectedMovie.title.$error" class="invalid-feedback">
                Title is required (max 200 characters).
              </div>
            </div>

            <div class="mb-3">
              <label for="movieDirector" class="form-label">Director:</label>
              <input type="text" id="movieDirector" class="form-control" v-model="selectedMovie.director">
            </div>

            <div class="mb-3">
              <label for="movieYear" class="form-label">Year:</label>
              <input type="number" id="movieYear" class="form-control" v-model="selectedMovie.year" :class="{'is-invalid': v$.selectedMovie.year.$error}">
              <div v-if="v$.selectedMovie.year.$error" class="invalid-feedback">
                Year must be between 1900 and 2200.
              </div>
            </div>

            <div class="mb-3">
              <label for="movieRate" class="form-label">Rate:</label>
              <input type="number" min=0 max=10 id="movieRate" class="form-control" v-model="selectedMovie.rate" required>
            </div>
          </div>

          <div class="modal-footer">
            <button type="button" data-bs-dismiss="modal" class="btn btn-secondary" @click="closeModal">Cancel</button>
            <button type="submit" class="btn btn-primary" @click="saveMovie">{{ isEditMode ? "Save changes" : "Add Movie" }}</button>
          </div>
        </div>
      </div>
    </div>
  </div>

</template>

<script>
  import axios from "axios";
  import { ref, computed } from "vue";
  import useVuelidate from "@vuelidate/core";
  import { required, minValue, maxValue, maxLength, numeric } from "@vuelidate/validators";
  import { Modal } from 'bootstrap';
  
  import Alert from './Alert.vue';

  export default {
    data() {
      return {
        isEditMode: false,
        isLoading: false,
        movies: [],
        selectedMovie: { id: null, title: '', director: '', year: '', rate: '' },
        modalInstance: null,
        errorMessage: "", 
        successMessage: "",
      };
    },
    setup() {return {v$: useVuelidate() }},
    validations(){
      return{
        selectedMovie: {
          title: {required, maxLength: maxLength(200),},
          year: {numeric, minValue: minValue(1900), maxValue: maxValue(2200),}
        }
      }
    },
    async mounted() {
      const modalElement = document.getElementById('staticBackdrop');
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
          this.isLoading = true;
          const response = await axios.get("/api/Movies");
          this.movies = response.data;
          this.isLoading = false;
        } catch (error) {
          console.error("Error fetching movies:", error);
          this.errorMessage = "Error downloading movies.";
          this.isLoading = false;
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
      async addNewMovie() {
        try{
          this.isEditMode = false;
          this.selectedMovie = { title: '', director: '', year: '', rate: '' };
          this.v$.$reset();
          this.openModal();
        }catch (error) {
          console.error("Error adding movies:", error);
          this.errorMessage = "Error adding movie.";
        }
      },
      async deleteMovie(id) {
        if (confirm('Do you want to delete this movie?')) {
          try{
            await axios.delete(`/api/Movies/${id}`);
            await this.fetchMovies();
          }catch (error) {
            console.error("Error fetching movies:", error);
            this.errorMessage = "Error deleting movie.";
          }
        }
      },
      editMovie(movie) {
        try{
          this.selectedMovie = { ...movie };
          this.isEditMode = true;
          this.v$.$reset();
          this.openModal();
        }catch (error) {
          console.error("Error editing movies:", error);
          this.errorMessage = "Error editing movie.";
        }
      },
      async saveMovie() {
        const isValid = await this.v$.$validate();
        if (!isValid) return;

        try {
          if (this.isEditMode) {
            await axios.put(`/api/Movies/${this.selectedMovie.id}`, this.selectedMovie);
          } else {
            await axios.post("/api/Movies", this.selectedMovie);
          }
          this.fetchMovies();
          this.closeModal();
          this.successMessage = "Movie saved successfully!";
          this.errorMessage = "";
          this.modalInstance?.show();
        } catch (error) {
          console.error('Error saving movie:', error);
          this.errorMessage = "Failed to save movie.";
          this.successMessage = "";
          this.modalInstance?.hide();
        }
      },
      async downloadMovies() {
        try {
          const response = await axios.post("/api/Movies/fetch");

          if (response?.data?.length > 0) {
            this.successMessage = "Movies downloaded successfully!";
            this.errorMessage = ""; 
            await this.fetchMovies();
          } else {
            this.errorMessage = "Unable to download movies, no new movies to download.";
            this.successMessage = ""; 
          }
        } catch (error) {
          console.error("Error downloading movies:", error);
          this.errorMessage = "Failed to download movies. Please try again later.";
          this.successMessage = ""; 
        }
      }
    },
    components: {
      Alert
    }
  };
</script>

    

