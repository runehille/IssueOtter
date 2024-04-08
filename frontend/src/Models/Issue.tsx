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
};

export type IssuePost = {
  projectKey: string;
  title: string;
  content: string;
};
