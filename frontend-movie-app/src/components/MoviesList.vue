<template>
  <div class="container mt-4">
    <h1>Movies List</h1>

    <!-- Komunikaty o błędach i sukcesach -->
    <div v-if="errorMessage" class="alert alert-danger" role="alert">
      {{ errorMessage }}
    </div>
    <div v-if="successMessage" class="alert alert-success" role="alert">
      {{ successMessage }}
    </div>

    <!-- Przycisk do dodania filmu i pobierania -->
    <div class="mb-3">
      <button @click="addNewMovie" class="btn btn-primary">Add Movie</button>
      <button @click="downloadMovies" class="btn btn-secondary ml-2">Download Movies</button>
    </div>

    <!-- Tabela filmów -->
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
        <tr v-for="movie in movies" :key="movie.id">
          <td>{{ movie.id }}</td>
          <td>{{ movie.title }}</td>
          <td>{{ movie.director }}</td>
          <td>{{ movie.year }}</td>
          <td>{{ movie.rate }}</td>
          <td>
            <button @click="editMovie(movie)" class="btn btn-warning btn-sm">Edit</button>
            <button @click="deleteMovie(movie.id)" class="btn btn-danger btn-sm ml-2">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal dla edycji i dodawania filmu -->
    <div class="modal fade" id="movieModal" tabindex="-1" aria-labelledby="movieModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="movieModalLabel">{{ isEditMode ? "Edit Movie" : "Add Movie" }}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="closeModal"></button>
          </div>
          <div class="modal-body">
            <!-- Title input -->
            <div class="mb-3">
              <label for="movieTitle" class="form-label">Title:</label>
              <input type="text" id="movieTitle" class="form-control" v-model="selectedMovie.title" :class="{'is-invalid': v$.title.$error}">
              <div v-if="v$.title.$error" class="invalid-feedback">
                Title is required (max 200 characters).
              </div>
            </div>

            <!-- Director input -->
            <div class="mb-3">
              <label for="movieDirector" class="form-label">Director:</label>
              <input type="text" id="movieDirector" class="form-control" v-model="selectedMovie.director">
            </div>

            <!-- Year input -->
            <div class="mb-3">
              <label for="movieYear" class="form-label">Year:</label>
              <input type="number" id="movieYear" class="form-control" v-model="selectedMovie.year" :class="{'is-invalid': v$.year.$error}">
              <div v-if="v$.year.$error" class="invalid-feedback">
                Year must be between 1900 and 2200.
              </div>
            </div>

            <!-- Rate input -->
            <div class="mb-3">
              <label for="movieRate" class="form-label">Rate:</label>
              <input type="number" min=0 max=10 id="movieRate" class="form-control" v-model="selectedMovie.rate">
            </div>
          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="closeModal">Cancel</button>
            <button type="button" class="btn btn-primary" @click="saveMovie">{{ isEditMode ? "Save changes" : "Add Movie" }}</button>
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

  export default {
    data() {
      return {
        isEditMode: false,
        movies: [],
        selectedMovie: { id: null, title: '', director: '', year: '', rate: '' },
        modalInstance: null,
        errorMessage: "", 
        successMessage: "",
      };
    },
    setup() {
      const selectedMovie = ref({
        title: "",
        director: "",
        year: null
      });

      // zasady walidacji
      const rules = computed(() => ({
        title: { required, maxLength: maxLength(200) },
        year: { required, numeric, minValue: minValue(1900), maxValue: maxValue(2200) }
      }));

      const v$ = useVuelidate(rules, selectedMovie);

      return { selectedMovie, v$ };
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
          this.errorMessage = "Błąd podczas pobierania filmów.";
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
        this.isEditMode = false;
        this.selectedMovie = { title: '', director: '', year: '', rate: '' };
        this.v$.$reset();
        this.openModal();
      },
      async deleteMovie(id) {
        if (confirm('Do you want to delete this movie?')) {
          await axios.delete(`/api/Movies/${id}`);
          await this.fetchMovies();
        }
      },
      editMovie(movie) {
        this.selectedMovie = { ...movie };
        this.isEditMode = true;
        this.v$.$reset();
        this.openModal();
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
          this.successMessage = "Film zapisano pomyślnie!";
          this.errorMessage = ""; 
        } catch (error) {
          console.error('Error saving movie:', error);
          this.errorMessage = "Nie udało się zapisać filmu.";
          this.successMessage = "";
        }
      },
      async downloadMovies() {
        try {
          const response = await axios.post("/api/Movies/fetch");

          // Sprawdź, czy odpowiedź zawiera nowe filmy
          if (response.data && response.data.length > 0) {
            this.successMessage = "Filmy zostały pomyślnie pobrane!";
            this.errorMessage = ""; 
            await this.fetchMovies();
          } else {
            this.errorMessage = "Nie można pobrać filmów, brak nowych filmów do pobrania.";
            this.successMessage = ""; 
          }
        } catch (error) {
          console.error("Error downloading movies:", error);
          this.errorMessage = "Nie udało się pobrać filmów. Spróbuj ponownie później.";
          this.successMessage = ""; 
        }
      }
    },
  };
</script>

    

