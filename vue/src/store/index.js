import { createStore } from 'vuex'
import transactions from '../store/modules/transactions'

const store = createStore({
    modules: {
        transactions
    }
});

export default store