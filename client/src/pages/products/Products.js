import styles from './Products.module.css'

function ProductsPage() {
    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>Products</h1>
                <button
                    className={styles['add-product-button']}
                >
                    Add New Product
                </button>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">Search:</label>
                    <input
                        id="search-input"
                        type="text"
                        // value={searchTerm}
                        placeholder="Search product"
                        className={`form-control ${styles['input-field']}`}
                    // onChange={(e) => {
                    //     setCurrentPage(1);
                    //     setSearchTerm(e.target.value);
                    // }}
                    />
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Sort by:</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                    // onChange={(e) => {
                    //     setCurrentPage(1);
                    //     setSorting(e.target.value);
                    // }}
                    >
                        <option value="0">Name (Ascending)</option>
                        <option value="1">Name (Descending)</option>
                        <option value="4">Price (Descending)</option>
                        <option value="5">Price (Ascending)</option>
                        <option value="4">Quantity (Descending)</option>
                        <option value="5">Quantity (Ascending)</option>
                        <option value="2">Delivery price (Descending)</option>
                        <option value="3">Delivery price (Ascending)</option>
                    </select>
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Products per Page :</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                    // onChange={(e) => {
                    //     setCurrentPage(1);
                    //     setClientsPerPage(e.target.value)
                    // }}
                    >
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                    </select>
                </div>
            </div>
        </div>
    );
}

export default ProductsPage;