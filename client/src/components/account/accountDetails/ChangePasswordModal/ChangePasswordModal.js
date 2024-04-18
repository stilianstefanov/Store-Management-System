import styles from './ChangePasswordModal.module.css';

function ChangePasswordModal() {
    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Change Password</h1>
                <form>
                    <input
                        type="password"
                        placeholder="Current password"
                        className={styles["Auth-input"]}
                    // value={currentPassword}
                    // onChange={(e) => setCurrentPassword(e.target.value)}
                    />
                    {validationErrors.currentPassword && <p className={styles["Error-message"]}>{validationErrors.currentPassword}</p>}
                    <input
                        type="password"
                        placeholder="New password"
                        className={styles["Auth-input"]}
                    // value={newPassword}
                    // onChange={(e) => setNewPassword(e.target.value)}
                    />
                    {validationErrors.newPassword && <p className={styles["Error-message"]}>{validationErrors.newPassword}</p>}
                    <input
                        type="password"
                        placeholder="Confirm password"
                        className={styles["Auth-input"]}
                    // value={confirmPassword}
                    // onChange={(e) => setConfirmPassword(e.target.value)}
                    />
                    {validationErrors.confirmPassword && <p className={styles["Error-message"]}>{validationErrors.confirmPassword}</p>}
                    <button type="submit" className={styles["Auth-button"]}>
                        Confirm
                    </button>
                </form>
            </div>
        </div>
    );
};

export default ChangePasswordModal;