import { useState } from "react";
import Button from "@mui/material/Button/Button";
import TextField from "@mui/material/TextField/TextField";
import { Task } from "../../../models/Task";
import styles from "./NewTaskModal.module.css";

interface NewTaskModalProps{
    onClose: () => void;
    onAddTask: (task: Task) => void;
}

const NewTaskModal: React.FC<NewTaskModalProps> = ( {onClose, onAddTask}) => {
    const [taskText, setTaskText] = useState('')

    const saveTask = () => {
        const newTask: Task = {
            text: taskText,
            statusId: 1
        }

        onAddTask(newTask);   
        onClose();    
    }

    return (
        <div className={styles.modalOverlay}>
            <div className={styles.modalContent}>
            <TextField
                id="standard-basic"
                label="Задача"
                variant="standard"
                value={taskText}
                onChange={(e) => setTaskText(e.target.value)} /> 
            <Button color="secondary" onClick={saveTask}>Сохранить</Button>
            </div>
        </div>
    )
}

export default NewTaskModal;