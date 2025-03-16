import { AuthResponse } from "../models/AuthResponse";
import { Task } from "../models/Task";
import { User } from "../models/User";

const API_URL = process.env.REACT_APP_API_URL;

const TASKS_PATH = "/api/Tasks";
const AUTH_PATH = "/api/Auth";

export const updateTask = async (task: Task): Promise<Task> => {
    const response = await fetch(`${API_URL}${TASKS_PATH}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        },
        body: JSON.stringify(task),
      });
    
      if (!response.ok) {
        throw new Error('Error update task status');
      }
    
      return response.json();
}

export const getTasks = async (): Promise<Task[]> => {
    const response = await fetch(`${API_URL}${TASKS_PATH}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        },
      });
    
      if (!response.ok) {
        throw new Error('Error update task status');
      }
    
      return response.json();
}

export const deleteTasks = async (id: number): Promise<void> => {
  const response = await fetch(`${API_URL}${TASKS_PATH}?id=${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      },
    });
  
    if (!response.ok) {
      throw new Error('Error update task status');
    }
  
    return;
}

export const addTask = async (task: Task): Promise<Task> => {
  const response = await fetch(`${API_URL}${TASKS_PATH}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token'),
    },
    body: JSON.stringify(task),
  });

  if (!response.ok) {
    throw new Error('Error update task status');
  }

  return response.json();
}

export const login = async (user: User): Promise<AuthResponse> => {
  const response = await fetch(`${API_URL}${AUTH_PATH}/login`, {
    method: `POST`,
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(user),
  });

  if (!response.ok) {
    throw new Error('Error update task status');
  }

  return response.json();
}

export const registration = async (user: User): Promise<AuthResponse> => {
  const response = await fetch(`${API_URL}${AUTH_PATH}/registration`, {
    method: `POST`,
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(user),
  });

  if (!response.ok) {
    throw new Error('Error update task status');
  }

  return response.json();
}

export default { updateTask, getTasks, deleteTasks, addTask, login, registration };