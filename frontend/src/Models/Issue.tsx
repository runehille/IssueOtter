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
  createdOn: string;
  lastUpdatedOn: string;
  comments: Comment[];
};

export type IssuePost = {
  projectKey: string;
  title: string;
  content: string;
};

export type Comment = {
  id: number;
  content: string;
  createdOn: string;
  createdById: number;
  issueId: number;
};
