<template>
  <div class="home-view">
    <div class="content">
      <h1>Welcome to My Game</h1>
      <button @click="navigateToGamePage">Go to Game</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRoute, useRouter } from "vue-router";

export default defineComponent({
  name: "HomeView",
  components: {
  },
  setup() {
    const route = useRoute();
    const router = useRouter();

    const navigateToGamePage = () => {
      router.push({ name: "GamePage" });
    };

    return {
      route,
      navigateToGamePage,
    };
  },
});
</script>

<style scoped>
.home-view {
  position: relative;
  width: 100%;
  height: 100%;
}

.content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  text-align: center;
}
</style>