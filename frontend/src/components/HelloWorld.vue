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
            <svg class="bd-placeholder-img card-img-top" width="100%" height="225" xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: Thumbnail"><title>Placeholder</title><rect width="100%" height="100%" fill="#55595c"/><text x="50%" y="50%" fill="#eceeef" dy=".3em">{{item.name}}</text></svg>
            <div class="card-body">
              <p class="card-text">{{item.summary}}</p>
              <div class="d-flex justify-content-between align-items-center">
                <div class="btn-group">
                  <button type="button" class="btn btn-sm btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="editClick(item)">编辑</button>
                  <button type="button" class="btn btn-sm btn-outline-secondary">删除</button>
                </div>
                <small class="text-muted">{{item.lastDateTime}}</small>
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
const API_URL ='https://localhost:7242/api/' 

import axios from 'axios';
export default {
  data() {
    return {
      Id:0,
      Name:"",
      Summary:"",
      modalTitle:"",
      rows: [ ],
      errors:[],
    }
  },
  methods:{
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
    },
    editClick(item){
        this.modalTitle="编辑";
        this.Id=item.id;
        this.Name=item.name;
        this.Summary = item.summary;
    },
    createClick(){
        axios.post(API_URL+"Meals",{
            name: this.Name,
            summary: this.Summary,
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
            summary:this.Summary
        })
        .then(()=>{
            this.refreshData();
        });
    },
    deleteClick(id){
        if(!confirm("Are you sure?")){
            return;
        }
        axios.delete(process.env.API_URL+"department/"+id)
        .then((response)=>{
            this.refreshData();
            alert(response.data);
        });

    },
  },

  // Fetches posts when the component is created.
  created() {
    this.refreshData();
  }
}
</script>