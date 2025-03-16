import { memo, useState } from "react";
import Button from '@mui/material/Button';
import { ThemeProvider } from "@mui/material/styles";
import { Task } from "../../models/Task";
import ThemeService from "../../data/ThemeService";
import ChangeStatusModal from "../modal/change-status-modal/ChangeStatusModal";
import styles from "./TodoItem.module.css";

interface TodoItemProps {
    name: string;
    task: Task;
    onUpdateTask: (task: Task) => void;
    onDeleteTask: (taskId: number) => void;
}

const TodoItem: React.FC<TodoItemProps> = memo(({name, task, onUpdateTask, onDeleteTask }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const { getButtonTheme, getButtonColor } = ThemeService();

    const deleteTask = () => {
        onDeleteTask(task.id!);
    }

    const openModal = () => {
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
    };

    return (
        <>
            <div className={styles.itemConteiner}>
                <ThemeProvider theme={getButtonTheme()}>
                    <Button
                            variant="contained"
                            color={getButtonColor(task.statusId!)}
                            onClick={openModal}>
                        {name}
                    </Button>
                </ThemeProvider>
                <div>{task.text}</div>
                <Button
                        variant="text"
                        onClick={deleteTask}>
                    Удалить
                </Button>
            </div>

            {isModalOpen && <ChangeStatusModal task={task} onClose={closeModal} onUpdateTask={onUpdateTask} />}
        </>
    )
});

export default TodoItem;