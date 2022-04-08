<template>
<div>
<div class="left ">
  <button type="button"
  class="btn btn-primary m-2 fload-end"
  data-bs-toggle="modal"
  data-bs-target="#exampleModal"
  @click="save()">
  选好了  
</button>
  <button type="button"
  class="btn btn-primary m-2 fload-end"
  data-bs-toggle="modal"
  data-bs-target="#exampleModal"
  @click="refreshData()">
  重选  
</button>
</div>
  <div class="album py-5 bg-light">
    <div class="container">
      <div class="row">
        <div class="col-md-4" v-for="item in rows" :key="item.id">
          <div class="card mb-4 shadow-sm" @click="selectMeal(item.id)">
            <div class="card-body" >
            <b-img class="card" :src="PhotoBasePath + item.photoPath"/>
              <p class="card-text">{{item.summary}}</p>
                <img class="icon" v-if="item.type==='M'" src="../assets/meat.png" />
                <img class="icon" v-else-if="item.type==='V'" src="../assets/vegetable.png" />
                <img class="icon" v-else-if="item.type==='S'" src="../assets/soup.png" />
                <img class="icon" v-else src="../assets/meal.png" />
              <div class="d-flex justify-content-between align-items-center">
                  <b-form-checkbox v-model="mealIds" :id="item.id" :value="item.id">
                  </b-form-checkbox>
                  <small class="text-muted">{{this.formatDate(item.lastDateTime)}}</small>
                </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
<div>Checked names: {{ mealIds }}</div>
</div>
</template>
<script>
const API_URL =process.env.VUE_APP_API_URL+'/api/' 
const PHOTO_BASE_URL = process.env.VUE_APP_API_URL+'/photos/'

import axios from 'axios';
import moment from 'moment'
export default {
  data() {
    return {
      rows: [ ],
      mealIds: [],
      PhotoBasePath:PHOTO_BASE_URL,
      errors:[],
    }
  },
  methods:{
    selectMeal(id){
      var selectedCheckbox = document.getElementById(id)
      if(selectedCheckbox.checked)
      {
        this.mealIds.pop(id);
      }
      else
      {
        this.mealIds.push(id);
      }
    },
    formatDate(value){
      moment.locale('zh-cn');
      return moment(value).format("LLLL");
    },
    save(){
        axios.post(API_URL+"TodaysMeals", this.mealIds
        )
        .then(response=>{
          this.rows = response.data;
          alert("选好了");
        })
      .catch(e => {
        this.errors.push(e)
      })
        ;
    },
    refreshData(){
      axios.get(API_URL+'TodaysMeals')
      .then(response => {
        // JSON responses are automatically parsed.
        this.rows = response.data
      })
      .catch(e => {
        this.errors.push(e)
      })
    },
  },
  // Fetches posts when the component is created.
  created() {
    this.refreshData();
  }
}
</script>