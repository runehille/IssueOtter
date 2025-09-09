export enum IssueStatus {
  ToDo = 0,
  InProgress = 1,
  InReview = 2,
  Done = 3,
  Closed = 4
}

export enum IssueType {
  Task = 0,
  Bug = 1
}

export enum IssuePriority {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
}

export type IssueGet = {
  id: number;
  title: string;
  key: string;
  status: IssueStatus;
  type: IssueType;
  priority: IssuePriority;
  content: string;
  assignee?: {
    email: string;
    firstName: string;
    lastName: string;
  };
  assigneeId: number;
  reporter: string;
  project: string;
  createdBy?: {
    email: string;
    firstname: string;
    lastname: string;
  };
  createdOn: string;
  lastUpdatedOn: string;
  comments: Comment[];
  labels: Label[];
};

export type IssuePost = {
  projectKey: string;
  title: string;
  content: string;
  status: IssueStatus;
  type: IssueType;
  priority: IssuePriority;
  labelIds?: number[];
};

export type IssueStatusUpdate = {
  status: IssueStatus;
};

export type IssueUpdate = {
  title: string;
  content: string;
  type: IssueType;
  status: IssueStatus;
  priority: IssuePriority;
  assigneeId: number;
};

export type Comment = {
  id: number;
  content: string;
  createdOn: string;
  createdBy: {
    id: number;
    email: string;
    firstName: string;
    lastName: string;
  };
  issueId: number;
};

export type Label = {
  id: number;
  name: string;
  color: string;
  description: string;
  projectId: number;
  createdOn: string;
};
