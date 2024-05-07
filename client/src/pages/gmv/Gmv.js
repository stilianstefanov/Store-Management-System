import styles from './Gmv.module.css'

function GmvPage() {



    return (
        <div className={`container ${styles['table-container']}`}>
            <h1 className={`text-center ${styles['title']}`}>GMV</h1>
            <div className="d-flex justify-content-center flex-wrap">
            </div>
        </div>
    );
};

export default GmvPage;