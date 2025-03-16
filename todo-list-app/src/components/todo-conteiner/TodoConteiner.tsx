import { useCallback, useEffect, useState } from "react";
import { Button } from "@mui/material";
import { addTask, deleteTasks, getTasks, updateTask } from "../../services/RequestService";
import TodoList from "../todo-list/TodoList";
import { Task } from "../../models/Task";
import TaskStatuses from "../../data/TaskStatuses";
import useHubConnection from "../hooks/useHubConnection";
import NewTaskModal from "../modal/new-task-modal/NewTaskModal";
import styles from "./TodoConteiner.module.css";

interface TodoConteinerProps {
    setIsAuthorized: React.Dispatch<React.SetStateAction<boolean>>
}

const TodoConteiner: React.FC<TodoConteinerProps> = ({ setIsAuthorized }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [tasks, setTasks] = useState<Task[]>([]);
    useHubConnection(setTasks, tasks);

    useEffect(() => {
        const fetchTasks = async () => {
            try {
                const tasks = await getTasks();
                setTasks(tasks);
            } catch (error) {
                console.error("Error fetching tasks:", error);
                setIsAuthorized(false);
                localStorage.removeItem('token');
            }
        };

        fetchTasks();
    }, []);

    const handleUpdateTask = useCallback(async (updatedTask: Task) => {
        try {
            const taskFromServer = await updateTask(updatedTask);

            setTasks((prevTasks) =>
                prevTasks.map((task) =>
                    task.id === taskFromServer.id
                        ? taskFromServer
                        : task
                )
            );
        } catch (error) {
            setIsAuthorized(false);
            localStorage.removeItem('token');
        }
    }, []);

    const handleDeleteTask = useCallback(async (taskId: number) => {
        try {
            await deleteTasks(taskId);

            setTasks((prevTasks) =>
                prevTasks.filter((task) => task.id !== taskId)
            );
        } catch (error) {
            setIsAuthorized(false);
            localStorage.removeItem('token');
        }
    }, []);

    const handleAddTask = useCallback(async (task: Task) => {
        try {
            const newTask = await addTask(task);

            setTasks((prevTasks) => [...prevTasks, newTask]);
        } catch (error) {
            setIsAuthorized(false);
            localStorage.removeItem('token');
        }
    }, []);

    const openModal = () => {
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
    };

    return (
        <>
            <div className={styles.block}>
                <Button color="secondary" onClick={openModal}>Добавить задачу</Button>
                <div className={styles.todoConteiner}>
                    {TaskStatuses.map((status) => (
                        <TodoList
                            key={status.statusId}
                            name={status.name}
                            tasks={tasks.filter(t => t.statusId == status.statusId)}
                            onUpdateTask={handleUpdateTask}
                            onDeleteTask={handleDeleteTask} />
                    ))}
                </div>
            </div>

            {isModalOpen && <NewTaskModal onClose={closeModal} onAddTask={handleAddTask} />}
        </>
    )
}

export default TodoConteiner;