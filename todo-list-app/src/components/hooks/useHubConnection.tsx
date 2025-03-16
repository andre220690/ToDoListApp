import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr"
import { Task } from "../../models/Task";

const API_URL = process.env.REACT_APP_API_URL;
const HUB_PATH = "/hub/task";

const useHubComponent = (
    setTasks: React.Dispatch<React.SetStateAction<Task[]>>,
    tasks: Task[]) => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

    useEffect(()=>{
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl(API_URL+HUB_PATH)
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    // TODO: тут баг. Идет двойное срабаывание перебора коллекции если пользователь производит действия, нужно на бэке исключать текущего пользователя из отправки
    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    console.log('SignalR Connected!');

                    connection.on('invokeDelete', (taskId) => {
                        setTasks((prevTasks) =>
                            prevTasks.filter((task) => task.id !== taskId)
                        );
                    });

                    connection.on('invokeUpdate', (updatedTask: Task) => {
                        setTasks((prevTasks) =>
                            prevTasks.map((task) =>
                                task.id === updatedTask.id
                                    ? updatedTask
                                    : task
                            )
                        );
                    });

                    connection.on('invokeAdd', (newTask) => {
                        if (tasks.filter(r => r.id === newTask.id).length != 0) {
                            setTasks((prevTasks) => 
                                [...prevTasks, newTask]);
                        }                        
                    });
                })
                .catch(err => console.error('SignalR Connection Error: ', err));
        }
    }, [connection]);

}

export default useHubComponent;