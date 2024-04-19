export type IssueGet = {
  title: string;
  key: string;
  status: string;
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
  status: string;
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
