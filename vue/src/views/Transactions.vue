<template>
    <v-container>
        
        <v-container class="text-right container">
        <div class="titulo text-left container">Transaction List</div>
        <div>
            
        <router-link :to="{ name: 'add' }">
            <v-btn prepend-icon="mdi-plus-thick">
                New Transaction
            </v-btn>
        </router-link>
        </div>
                
        </v-container>
        <div class="">
            <v-table class="styled-table" height="350px" fixed-header>
                <thead>
                    <tr class="text-center">
                        <th></th>
                        <th class="text-center">Transaction ID</th>
                        <th class="text-center">Amount</th>
                        <th class="text-center">Name</th>
                        <th class="text-center">Tran Date</th>
                        <th class="text-center">Status</th>
                        <th class="text-center">V</th>
                        <th class="text-center">R</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                      v-for="item in allTransactions"
                      :key="item.id"
                    >
                      <td><v-checkbox :disabled="item.Status!=0" @change="AppendToList(item.Id)"/></td>
                      <td>{{ item.Id }}</td>
                      <td>{{ item.Amount }}</td>
                      <td>{{ item.Name }}</td>
                      <td>{{ WriteDate(item.Date)}}</td>
                      <td>{{ setStatusName(item.Status) }}</td>
                      <td><v-btn v-if="item.Status==0" @click="StatusChange(item,2)" icon="mdi mdi-minus" color="red"/></td>
                      <td><v-btn v-if="item.Status==1" @click="StatusChange(item,3)" icon="mdi-keyboard-return" color="blue"/></td>
                    </tr>
                </tbody>
            </v-table>
        </div>
        <v-container class="text-right">
            <v-btn prepend-icon="mdi-plus-box-multiple" :disabled="ids.idList.length == 0" @click="ApproveSelected" class="me-4">
                Approve Selected Items
            </v-btn>
            <v-btn prepend-icon="mdi-check-all" @click="setAllPendings">
                Approve all Pendings
            </v-btn>
        </v-container>
    </v-container>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import moment from 'moment';

export default {
    name:"TransactionsList",
    data() {
      return {
        ids: {
            idList: []
        },
      };
    },
    methods: {
        ...mapActions(['fetchTransactions','changeStatus','setAllPendings', 'ApproveAllSelected']),
        setStatusName(i){
            switch(i){
                case 0:
                    return "Pending"
                case 1:
                    return "Approved"
                case 2:
                    return "Voided"
                case 3:
                    return "Refunded"
            }
        },
        WriteDate(value){
         if (value) {
           return moment(String(value)).format('MM-DD-YYYY')
          }
        },
        AppendToList(id){
            if(this.ids.idList.indexOf(id) == -1)
            {
                this.ids.idList.push(parseInt(id))
            }
            else
            {
                this.ids.idList.splice(this.ids.idList.indexOf(id), 1)
            }
            console.log(this.ids)
        },
        StatusChange(item, status){
            item.Status = status,
            this.changeStatus(item)
        },
        ApproveSelected(){
            this.ApproveAllSelected(this.ids)
            this.ids.idList = []
        }
    },
    computed: {
        rows() {
        return this.allTransactions.length
        },
        ...mapGetters(['allTransactions']),
    },
    created () {
        this.fetchTransactions();
    }
}
</script>

<style lang="scss">

</style>