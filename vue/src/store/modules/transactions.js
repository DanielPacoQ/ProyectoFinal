import axios from 'axios'

const state = {
    transactions: []
};

const getters = {
    allTransactions: state => state.transactions
};

const actions = {
    async fetchTransactions({ commit }){
        const response = await axios.get('https://localhost:44392/api/Transaction');
        commit('setTransactions',response.data);
    },
    async addTransaction({commit}, transaction){
        const response = await axios.post('https://localhost:44392/api/Transaction',transaction );
        commit('newTransaction',response.data);
    },
    async setAllPendings({commit}){
        const response = await axios.put('https://localhost:44392/api/Transaction/all');
        commit('setTransactions',response.data);
    },
    async ApproveAllSelected({commit}, ids){
        console.log(ids)
        const response = await axios.put('https://localhost:44392/api/Transaction/ids', ids );
        commit('setTransactions',response.data);
    },
    async changeStatus({commit}, updTransaction){
        await axios.put(`https://localhost:44392/api/Transaction/${updTransaction.Id}/${updTransaction.Status}`,updTransaction);
        commit('updateTransaction',updTransaction);
    }
};

const mutations = {
    setTransactions: (state, transactions) => (state.transactions = transactions),
    newTransaction: (state,transaction) => state.transactions.unshift(transaction),
    updateTransaction:(state, updTransaction) => {
        const index = state.transactions.findIndex(transaction => transaction.Id === updTransaction.Id);
        if(index !== -1){
            state.transactions.splice(index,1,updTransaction);
        }
    }
};

export default {
    state,
    getters,
    actions,
    mutations
}