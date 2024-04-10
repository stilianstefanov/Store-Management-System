import styles from './DeleteClientModal.module.css';
import { useState } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import * as ClientService from '../../../../../services/clientService'

function DeleteClientModal({ clientId, closeModal, refreshClients }) {
    return (
        <div>
            <div className={styles["container"]}>
                <h1 className={styles["header"]}>Are you sure?</h1>
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={closeModal}>
                        Cancel
                    </button>
                    <button className={styles["button-confirm"]}>
                        Confirm
                    </button>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default DeleteClientModal;