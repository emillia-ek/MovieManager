<template>
  <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="staticBackdropLabel">{{ isEditMode ? "Edit Movie" : "Add Movie" }}</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="$emit('closeModal')"></button>
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
            <input type="number" id="movieRate" class="form-control" v-model="selectedMovie.rate">
          </div>
        </div>

        <div class="modal-footer">
          <button type="button" data-bs-dismiss="modal" class="btn btn-secondary" @click="$emit('closeModal')">Cancel</button>
          <button type="submit" class="btn btn-primary" @click="saveMovie">{{ isEditMode ? "Save changes" : "Add Movie" }}</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import useVuelidate from "@vuelidate/core";
  import { required, minValue, maxValue, maxLength, numeric } from "@vuelidate/validators";

  export default{
      props:{
          selectedMovie:{
              type: Object,
              requied: true
          },
          isEditMode:{
              type: Boolean
          }
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
      methods:{
        async saveMovie(){
          const isValid = await this.v$.$validate();
          if (!isValid) return;

          this.v$.$reset();
          this.$emit('saveMovie', this.selectedMovie);
        }
      }
  }
  
</script>