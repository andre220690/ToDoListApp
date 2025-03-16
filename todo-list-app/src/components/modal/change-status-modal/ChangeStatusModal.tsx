import { Button, ThemeProvider } from "@mui/material";
import { Task } from "../../../models/Task";
import ThemeService from "../../../data/ThemeService";
import TaskStatuses from "../../../data/TaskStatuses";
import styles from "./ChangeStatusModal.module.css";

interface ChangeStatusModalProps {
    task: Task;
    onClose: () => void;
    onUpdateTask: (task: Task) => void;
}

const ChangeStatusModal: React.FC<ChangeStatusModalProps> = ({ task, onClose, onUpdateTask }) => {
    const { getButtonTheme, getButtonColor } = ThemeService();

    const setStatus = (event: { currentTarget: { value: any; }; }) => { // TODO: страшно выглядит
        const newStatus = event.currentTarget.value;
        if (newStatus != task.statusId) {
            task.statusId = newStatus;
            onUpdateTask(task)
        }

        onClose();
    }

    return (
        <div className={styles.modalOverlay}>
            <div className={styles.modalContent}>
                <div>Новый статус</div>
                <ThemeProvider theme={getButtonTheme()}>
                    {TaskStatuses.map((status, index) => (
                        <Button
                            key={index}
                            variant="contained"
                            value={status.statusId}
                            color={getButtonColor(status.statusId)}
                            onClick={setStatus}>
                            {status.name}
                        </Button>
                    ))}
                </ThemeProvider>
                <Button color="secondary" onClick={onClose}>Закрыть</Button>
            </div>
        </div>
    )

}

export default ChangeStatusModal;