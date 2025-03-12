<template>
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
            <button @click="$emit('edit-movie', movie)" class="btn btn-secondary btn-sm">Edit</button>
            <button @click="$emit('delete-movie', movie.id)" class="btn btn-danger btn-sm m-2">Delete</button>
          </td>
        </tr>
      </tbody>

    </table>
</template>
<script>
    export default{
        props: {
            movies: {
                type: Array
            },
            isLoading:{
                type: Boolean
            }
        }
    }
</script>