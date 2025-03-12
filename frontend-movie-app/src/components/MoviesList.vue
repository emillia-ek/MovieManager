<template>
  <div class="container mt-4">
    <h1 class="display-3 mt-5">Movies List</h1>

    <Alert :message="errorMessage" type="danger"/>
    <Alert :message="successMessage" type="success"/>

    <div class="mb-3">
      <button @click="addNewMovie" class="btn btn-primary ">Add Movie</button>
      <button @click="downloadMovies" class="btn btn-secondary m-4 ">Download Movies</button>
    </div>

    <MovieTable :isLoading="isLoading" :movies="movies" @edit-movie = "editMovie" @delete-movie = "deleteMovie"/>
    <MovieModal :selectedMovie="selectedMovie" :isEditMode="isEditMode" :v$="v$" @save-movie="saveMovie" close-modal="closeModal"/>
  </div>

</template>

<script>
  import axios from "axios";
  import { ref, computed } from "vue";
  import { Modal } from 'bootstrap';
  
  import useVuelidate from "@vuelidate/core";
  import { required, minValue, maxValue, maxLength, numeric } from "@vuelidate/validators";
  
  import Alert from './Alert.vue';
  import MovieTable from './Movie-table.vue';
  import MovieModal from './Modal.vue';

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
      Alert,
      MovieTable,
      MovieModal
    }
  };
</script>

    

