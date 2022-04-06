<template>
<div class="left ">
</div>
  <div class="album py-5 bg-light">
    <div class="container">
      <div class="row">
        <div class="col-md-4" v-for="item in rows" :key="item.Id">
          <div class="card mb-4 shadow-sm">
            <div class="card-body">

            <img width="250px" height="250px" :src="PhotoBasePath+ item.photoPath"/>
              <p class="card-text">{{item.summary}}</p>
              <div class="d-flex justify-content-between align-items-center">
                <small class="text-muted">{{this.formatDate(item.lastDateTime)}}</small>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
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
      Id:0,
      Name:"",
      Summary:"",
      modalTitle:"",
      LastDateTime:"",
      PhotoFileName:"anonymous.png",
      PhotoBasePath:PHOTO_BASE_URL,
      rows: [ ],
      errors:[],
    }
  },
  methods:{
    formatDate(value){
      moment.locale('zh-cn');
      return moment(value).format("LLLL");
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