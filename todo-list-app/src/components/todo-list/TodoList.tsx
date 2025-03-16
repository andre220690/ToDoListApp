import { Task } from "../../models/Task";
import TodoItem from "../todo-item/TodoItem";
import styles from "./TodoList.module.css";

interface TodoListProps {
    name: string;
    tasks: Task[];
    onUpdateTask: (task: Task) => void;
    onDeleteTask: (taskId: number) => void;
  }
  
const TodoList: React.FC<TodoListProps> = ({name, tasks, onUpdateTask, onDeleteTask}) => {

    return(
        <div className={styles.todoList}>
            <div>{name}</div>
            {tasks.map((task)=>(
                <TodoItem key={task.id} name={name} task={task} onUpdateTask={onUpdateTask} onDeleteTask={onDeleteTask}/>
            ))}
        </div>
    )
}

export default TodoList;