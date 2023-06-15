<template>
  <div class="home-view">
    <div class="content">
      <v-title class="title ma-4">SPACE ROCKS</v-title>
      <button class="play-button" @click="navigateToGamePage">PLAY</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRoute, useRouter } from "vue-router";

export default defineComponent({
  name: "HomeView",
  components: {},
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
  top: 35%; 
  left: 50%;
  transform: translate(-50%, -50%);
  text-align: center;
}

.title {
  white-space: nowrap;
  color: yellow;
  font-size: 80px;
}

.play-button {
  font-size: 24px;
  width: 300px;
  height: 60px;
  background-color: #f0f0f0;
  border: none;
  border-radius: 10px;
  cursor: pointer;
  outline: none;

}
</style>
