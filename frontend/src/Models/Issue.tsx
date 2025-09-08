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

export type IssueGet = {
  title: string;
  key: string;
  status: IssueStatus;
  type: IssueType;
  content: string;
  assignee: {
    email: string;
    firstName: string;
    lastName: string;
  };
  assigneeId: number;
  reporter: string;
  project: string;
  createdBy: {
    email: string;
    firstname: string;
    lastname: string;
  };
  createdOn: string;
  lastUpdatedOn: string;
  comments: Comment[];
};

export type IssuePost = {
  projectKey: string;
  title: string;
  content: string;
  status: IssueStatus;
  type: IssueType;
};

export type IssueStatusUpdate = {
  status: IssueStatus;
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
