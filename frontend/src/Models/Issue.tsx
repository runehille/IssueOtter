export type IssueGet = {
  title: string;
  key: string;
  status: string;
  content: string;
  assignee: string;
  assigneeId: number;
  reporter: string;
  project: string;
};

export type IssuePost = {
  projectKey: string;
  title: string;
  content: string;
};
