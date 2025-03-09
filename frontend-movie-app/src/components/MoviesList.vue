<template>
  <div>
    <h1>Movies List</h1>
    <button @click="addNewMovie()">Add Movie</button>
    <button @click="downloadMovies()">Download Movies</button>
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
            <h5 class="modal-title">{{ isEditMode ? "Edit movie" : "Add movie" }}</h5>
            <button type="button" class="btn-close" @click="closeModal"></button>
          </div>
          <div class="modal-body">
            <div class="mb-3">
              <label for="movieTitle" class="form-label">Title:</label>
              <input type="text" id="movieTitle" class="form-control" v-model="selectedMovie.title">
              <small v-if="v$.title.$error" class="text-danger">Title is required (max 200 characters).</small>
            </div>
            <div class="mb-3">
              <label for="movieDirector" class="form-label">Director:</label>
              <input type="text" id="movieDirector" class="form-control" v-model="selectedMovie.director">
            </div>
            <div class="mb-3">
              <label for="movieYear" class="form-label">Year:</label>
              <input type="number" id="movieYear" class="form-control" v-model="selectedMovie.year">
              <small v-if="v$.year.$error" class="text-danger">Year must be between 1900 and 2200.</small>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="closeModal">Cancel</button>
            <button type="button" class="btn btn-primary" @click="saveMovie">{{ isEditMode ? "Save changes" : "Add movie" }}</button>
          </div>
        </div>
      </div>
    </div> <!-- Tylko jedno zamknięcie, poprawione! -->
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
          modalInstance: null
        };
      },
      setup() {
        const selectedMovie = ref({
          title: "",
          director: "",
          year: null
        });

        //zasady walidacji
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
          this.v$.$reset(); 
          this.openModal();
        },
        async deleteMovie(id) {
          if (confirm('Do you want to delete this movie?')){
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
            alert('Movie saved successfully!');
          } catch (error) {
            console.error('Error saving movie:', error);
            alert('Failed to save movie');
          }
        },
        async downloadMovies(){
          await axios.post("/api/Movies/fetch");
          await this.fetchMovies();
        }
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
