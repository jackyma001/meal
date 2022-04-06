<template>
<div class="left ">
  <button type="button"
  class="btn btn-primary m-2 fload-end"
  data-bs-toggle="modal"
  data-bs-target="#exampleModal"
  @click="addClick()">
  新菜  
</button>
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
                <div class="btn-group">
                  <button type="button" class="btn btn-sm btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="editClick(item)">编辑</button>
                  <button type="button" class="btn btn-sm btn-outline-secondary" @click="deleteClick(item.id)" >删除</button>
                </div>
                <small class="text-muted">{{this.formatDate(item.lastDateTime)}}</small>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
<div class="modal-dialog modal-lg modal-dialog-centered">
<div class="modal-content">
    <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"
        aria-label="Close"></button>
    </div>
    <div class="modal-body">
        <div class="input-group mb-3">
            <span class="input-group-text">菜名</span>
            <input type="text" class="form-control" v-model="Name">
        </div>
        <div class="input-group mb-3">
            <span class="input-group-text">描述</span>
            <input type="text" class="form-control" v-model="Summary">
        </div>
        <div class="p-2 w-50 bd-highlight">
            <img width="250px" height="250px" :src="PhotoBasePath+PhotoFileName"/>
            <input class="m-2" type="file" @change="imageUpload">
        </div>
        <button type="button" @click="createClick()"
        v-if="Id==0" class="btn btn-primary" data-bs-dismiss="modal">
        保存
        </button>
        <button type="button" @click="updateClick()" data-bs-dismiss="modal"
        v-if="Id!=0" class="btn btn-primary">
        修改
        </button>
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
      axios.get(API_URL+'Meals')
      .then(response => {
        // JSON responses are automatically parsed.
        this.rows = response.data
      })
      .catch(e => {
        this.errors.push(e)
      })
    },
    addClick(){
        this.modalTitle="新菜";
        this.Id=0;
        this.Name="";
        this.Summary ="";
        this.PhotoFileName="anonymous.png";
    },
    editClick(item){
        this.modalTitle="编辑";
        this.Id=item.id;
        this.Name=item.name;
        this.Summary = item.summary;
        this.LastDateTime = item.lastDateTime;
        this.PhotoFileName = item.photoPath;
    },
    createClick(){
        axios.post(API_URL+"Meals",{
            name: this.Name,
            summary: this.Summary,
            photoPath: this.PhotoFileName,
            lastDateTime: new Date()
        })
        .then(()=>{
            this.refreshData();
        })
      .catch(e => {
        this.errors.push(e)
      })
        ;
    },
    updateClick(){
        axios.put(API_URL+"Meals/"+ this.Id,{
            id:this.Id,
            name:this.Name,
            photoPath:this.PhotoFileName,
            summary:this.Summary,
            lastDateTime:this.LastDateTime
        })
        .then(()=>{
            this.refreshData();
        });
    },
    deleteClick(id){
        if(!confirm("Are you sure?")){
            return;
        }
        axios.delete(API_URL+"Meals/"+id)
        .then(()=>{
            this.refreshData();
        });

    },
  imageUpload(event){
        let formData=new FormData();
        formData.append('file',event.target.files[0]);
        axios.post(
            API_URL+"Meals/savefile",
            formData)
            .then((response)=>{
                this.PhotoFileName=response.data;
            });
    }
  },

  // Fetches posts when the component is created.
  created() {
    this.refreshData();
  }
}
</script>