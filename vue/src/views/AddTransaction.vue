<template>
    <v-container >
        <v-container class="text-left container">
            <h1 class="title">New Transaction</h1>
        </v-container>
        <v-container class="card">
            <v-form>
                <v-row>
                    <v-col cols="12" md="1"/>
                    <v-col cols="12" md="9">
                    <v-text-field 
                    oninput="this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');"
                    v-model="form.Amount"
                    label="Amount"
                    />
                    </v-col>
                    <v-col cols="12" md="1">
                        <v-icon v-if="v$.Amount.$error" class="icon-center" size="large" color="red">mdi mdi-alert</v-icon>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="1"/>
                    <v-col cols="12" md="9">
                    <v-text-field
                    label="CardHolder Name"
                    v-model="form.Name"
                    />
                    </v-col>
                    <v-col cols="12" md="1">
                        <v-icon v-if="v$.Name.$error" class="icon-center" size="large" color="red">mdi mdi-alert</v-icon>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="1"/>
                    <v-col cols="12" md="7">
                        <v-text-field
                        label="Card Number" 
                        oninput="this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');"
                        v-model="form.card" maxlength="16"/>  
                    </v-col>
                    <v-col cols="12" md="2">
                        <img v-if="form.card.charAt(0) == 3" class="logo" src="../assets/american.png">
                        <img v-if="form.card.charAt(0) == 4" class="logo" src="../assets/visa.png">
                        <img v-if="form.card.charAt(0) == 5" class="logo" src="../assets/mastercard.png">
                    </v-col>
                    <v-col cols="12" md="1">
                        <v-icon v-if="v$.card.$error" class="icon-center" size="large" color="red">mdi mdi-alert</v-icon>
                    </v-col>
                </v-row>
                
                <v-row>
                    <v-col cols="12" md="1"/>
                    <v-col cols="12" md="2">
                        <v-select
                        :items="months"
                        label="MM"
                        v-model="form.MM"
                        required/>
                    </v-col>
                    <v-col cols="12" md="1">
                        <v-icon v-if="v$.MM.$error" class="icon-center" size="large" color="red">mdi mdi-alert</v-icon>
                    </v-col>
                    <v-col cols="12" md="2">
                        <v-select
                        :items="years"
                        label="YYYY"
                        v-model="form.YYYY"
                        required/>
                    </v-col>
                    <v-col cols="12" md="1">
                        <v-icon v-if="v$.YYYY.$error" class="icon-center" size="large" color="red">mdi mdi-alert</v-icon>
                    </v-col>
                    <v-col cols="12" md="1"/>
                    <v-col cols="12" md="2">
                        <v-text-field
                        label="CVV"
                        oninput="this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');"
                        v-model="form.CVV"
                        maxlength="3"
                        />  
                    </v-col>
                    <v-col cols="12" md="1">
                        <v-icon v-if="v$.CVV.$error" class="icon-center" size="large" color="red">mdi mdi-alert</v-icon>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="8"/>
                    
                    <v-col cols="12" md="3">
                        <v-btn prepend-icon="mdi-pencil-plus" block @click="SubmitTransaction">Submit</v-btn>
                    </v-col>
                </v-row>
                
            </v-form>

        </v-container>
    </v-container>
</template>

<script>
import { useVuelidate } from '@vuelidate/core'
import { required , between, minLength, numeric} from '@vuelidate/validators'
import { mapActions } from 'vuex'
import { reactive } from 'vue'
export default {
    data() {
      return {
        years: [],
        today : new Date,
        months: ['01','02','03','04','05','06','07','08','09','10','11','12'],
        transaction: 
            {
                Id: 0,
                Amount: '',
                Name: '',
                Date: new Date(),
                Status: 0,
            },
      };
    },
    setup () {
            const form = 
                reactive({
                    Amount: '',
                    Name: '',
                    card: '',
                    MM: '',
                    YYYY: '',
                    CVV: ''
                })
            const rules = {
                    Amount: {required, numeric},
                    Name: {required},
                    card: {required, betweenValue: between(3000000000000000, 5999999999999999), numeric, minLength: minLength(16)},
                    MM: {required},
                    YYYY: {required},
                    CVV: {required, numeric, minLength: minLength(3)}
                }

            const v$ = useVuelidate(rules, form)

            return { form, v$ }
        },
    methods: {
        ...mapActions(['addTransaction']),
        getYears(){
            for (let i = 0; i < 6; i++) {
                this.years.push(this.today.getFullYear() + i)
            }
        },
        async SubmitTransaction(){
            this.v$.$validate()
            if (!this.v$.$error) 
            {
                if(this.form.YYYY > this.today.getFullYear() || (this.form.YYYY == this.today.getFullYear() && parseInt(this.form.MM) > this.today.getMonth() + 1))
                {
                    await this.createTransaction()
                    this.$router.push("/");
                }
                else{
                    alert('Credit Card Expired')
                }
            } 
            else {
                alert('Form failed validation')
            }
           
        },
        async createTransaction(){
            this.transaction.Amount = this.form.Amount
            this.transaction.Name = this.form.Name
            this.addTransaction(this.transaction)
        }
    },
    created () {
        this.getYears();
    }

}
</script>

<style lang="scss">

</style>